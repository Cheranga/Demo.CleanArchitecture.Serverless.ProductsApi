using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Domain;
using Products.Domain.Commands;
using Products.Domain.Models;

namespace Products.DataAccess.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
    {
        private const string InsertCommandSql = "insert into Products (Code, Name) " +
                                                "output inserted.Id, inserted.Code, inserted.Name " +
                                                "values (@Code, @Name)";


        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<CreateProductCommandHandler> _logger;


        public CreateProductCommandHandler(IDbConnectionFactory dbConnectionFactory, ILogger<CreateProductCommandHandler> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<Result<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                using (var connection = _dbConnectionFactory.GetConnection())
                {
                    var upsertedProducts = await connection.QueryAsync<Product>(InsertCommandSql, command);
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