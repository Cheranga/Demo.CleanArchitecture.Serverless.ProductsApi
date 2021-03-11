using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Application;
using Products.Domain;
using Products.Domain.Commands;
using Products.Domain.Models;
using Products.Domain.Queries;

namespace Products.DataAccess.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
    {
        private const string InsertCommandSql = "insert into Products (Code, Name) " +
                                             "output inserted.Id, inserted.Code, inserted.Name " +
                                             "values (@Code, @Name)";


        private const string UpdateCommandSql = "update Products set Name=@Name " +
                                             "output inserted.Id, inserted.Code, inserted.Name " +
                                             "where Code=@Code";

        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IMediator _mediator;


        public CreateProductCommandHandler(IDbConnectionFactory dbConnectionFactory, IMediator mediator, ILogger<CreateProductCommandHandler> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<Result<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var getProductQuery = new GetProductByCodeQuery
                {
                    CorrelationId = command.CorrelationId,
                    Code = command.Code
                };

                var getProductOperation = await _mediator.Send(getProductQuery, cancellationToken);
                if (!getProductOperation.Status)
                {
                    _logger.LogError("Error when getting the product information.");
                    return Result<Product>.Failure("", "Error when getting the product information.");
                }

                var dataCommand = getProductOperation.Data == null ? InsertCommandSql : UpdateCommandSql;

                using (var connection = _dbConnectionFactory.GetConnection())
                {
                    var upsertedProducts = await connection.QueryAsync<Product>(dataCommand, command);
                    var upsertedProduct = upsertedProducts.FirstOrDefault();
                    if (upsertedProduct == null)
                    {
                        _logger.LogError("Error when upserting product {command}", command);
                        return Result<Product>.Failure("", "Error occured when upserting product.");
                    }

                    return Result<Product>.Success(upsertedProduct);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occured when upserting product {command}", command);
            }

            return Result<Product>.Failure("", "Error occured when upserting product.");
        }
    }
}