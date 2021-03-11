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
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Product>>
    {
        private const string UpdateCommandSql = "update Products set Name=@Name " +
                                                "output inserted.Id, inserted.Code, inserted.Name " +
                                                "where Code=@Code";

        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IDbConnectionFactory dbConnectionFactory, ILogger<UpdateProductCommandHandler> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<Result<Product>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                using (var connection = _dbConnectionFactory.GetConnection())
                {
                    var upsertedProducts = await connection.QueryAsync<Product>(UpdateCommandSql, command);
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