using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Application;
using Products.Domain;

namespace Products.DataAccess.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
    {
        private const string InsertCommandSql = "insert into Products (ProductCode, ProductName) " +
                                             "output inserted.Id, inserted.ProductCode, inserted.ProductName " +
                                             "values (@ProductCode, @ProductName)";


        private const string UpdateCommandSql = "update Products set ProductCode=@ProductCode, ProductName=@ProductName " +
                                             "output inserted.Id, inserted.ProductCode, inserted.ProductName " +
                                             "where ProductCode=@ProductCode";

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
                return Result<Product>.Success(new Product());

                var getProductOperation = await _mediator.Send(new GetProductByCodeQuery{Code = command.Code}, cancellationToken);
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