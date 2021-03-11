using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Products.Application;
using Products.Domain;

namespace Products.DataAccess.QueryHandlers
{
    public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, Result<Product>>
    {
        private const string SqlQuery = "select * from Products where productcode=@productCode";
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ILogger<GetProductByCodeQueryHandler> _logger;

        public GetProductByCodeQueryHandler(IDbConnectionFactory dbConnectionFactory, ILogger<GetProductByCodeQueryHandler> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<Result<Product>> Handle(GetProductByCodeQuery query, CancellationToken cancellationToken)
        {
            try
            {
                using (var connection = _dbConnectionFactory.GetConnection())
                {
                    var commandDefinition = new CommandDefinition(SqlQuery, new {productCode = query.Code});
                    var product = await connection.QueryFirstOrDefaultAsync<Product>(commandDefinition);

                    return Result<Product>.Success(product);
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occured when getting the product {query}", query);
            }

            return Result<Product>.Failure("", "Error occured when getting the product.");
        }
    }
}