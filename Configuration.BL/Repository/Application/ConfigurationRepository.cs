using Configuration.BL.Helper;
using Configuration.DAL.Entity;
using Configuration.DAL.Enumeration;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Configuration.BL.Repository.Application
{
    public class ConfigurationRepository : IBaseRepository<ConfigurationEntity>
    {
        private IConfigHelper configHelper;

        public ConfigurationRepository(IConfigHelper configHelper)
        {
            this.configHelper = configHelper;
        }

        public OperationResult<bool> Delete(int id)
        {
            var result = new OperationResult<bool>();

            var applicationEntity = this.GetOne(id);

            string query = "UPDATE Configuration SET IsActive = 0 WHERE Id = @Id";

            if (applicationEntity != null)
            {
                using (var conn = new SqlConnection(this.configHelper.ConfigurationServiceDb))
                {
                    try
                    {
                        var queryResult = conn.Execute(query, new { Id = id });

                        result.IsSuccess = queryResult > 0;
                        result.Message = "Success";
                    }
                    catch (Exception ex)
                    {
                        result.IsSuccess = false;
                        result.Message = ex.Message;
                    }
                }
            }

            return result;
        }

        public OperationResult<bool> Edit(int id, ConfigurationEntity item)
        {
            var result = new OperationResult<bool>();


            var query = "UPDATE Configuration SET ApplicationId=@ApplicationId, DataType=@DataType, [Key]=@Key, Value=@Value, IsActive=@IsActive, CreatedDate=@CreatedDate, IsNew=@IsNew, IsProcessed=@IsProcessed  WHERE Id = @Id";
            using (var conn = new SqlConnection(this.configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var queryResult = conn.ExecuteScalar<int>(query, new
                    {
                        Id = item.Id,
                        Key = item.Key,
                        Value = item.Value,
                        DataType = item.DataType,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        ApplicationId = item.ApplicationId,
                        IsProcessed = item.IsProcessed,
                        IsNew = item.IsNew
                    }) > 0;

                    result.IsSuccess = queryResult;
                    result.Message = "Success";
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    result.IsSuccess = false;
                }
            }

            return result;
        }

        public OperationResult<List<ConfigurationEntity>> GetActiveItems()
        {
            var resultList = new OperationResult<List<ConfigurationEntity>>();
            resultList = new OperationResult<List<ConfigurationEntity>>();

            string whereClause = string.Empty;

            var query = $"SELECT [Id], [ApplicationId], [DataType], [Key] ,[Value] ,[IsActive], [CreatedDate] FROM [ConfigurationServer].[dbo].[Configuration]) Where IsActive = 1";

            using (var conn = new SqlConnection(configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var result = conn.Query<ConfigurationEntity>(query).OrderBy(n => n.Name).ToList();

                    resultList.Operation = result;
                    resultList.IsSuccess = result.Count > 0 ? true : false;
                    resultList.Message = result.Count > 0 ? "Success" : "Failed";
                }
                catch (Exception ex)
                {
                    resultList.IsSuccess = false;
                    resultList.Message = ex.Message;
                }
            }

            return resultList;
        }

        public OperationResult<List<ConfigurationEntity>> GetItems()
        {
            var resultList = new OperationResult<List<ConfigurationEntity>>();
            resultList = new OperationResult<List<ConfigurationEntity>>();
            
            var query = @"SELECT c.[Id], 
                                 a.[Name] ,
                                 c.[ApplicationId], 
                                 c.[DataType],
                                 c.[Key] ,
                                 c.[Value] ,
                                 c.[IsActive] as ConfigurationState,
                                 a.[IsActive],
                                 c.[IsNew],
                                 c.[CreatedDate]
                                 FROM [ConfigurationServer].[dbo].[Configuration] as c  
                          JOIN [ConfigurationServer].[dbo].[Application] as a 
                          ON a.Id = c.ApplicationId";

            using (var conn = new SqlConnection(configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var result = conn.Query<ConfigurationEntity>(query).OrderBy(n => n.Name).ToList();

                    resultList.Operation = result;
                    resultList.IsSuccess = result.Count > 0 ? true : false;
                    resultList.Message = result.Count > 0 ? "Success" : "Failed";
                }
                catch (Exception ex)
                {
                    resultList.IsSuccess = false;
                    resultList.Message = ex.Message;
                }
            }


            return resultList;
        }

        public OperationResult<ConfigurationEntity> GetOne(int id)
        {
            var result = new OperationResult<ConfigurationEntity>();

            string query = "SELECT TOP 1 Id, ApplicationId, DataType, [Key], Value, IsActive, IsNew, IsProcessed, CreatedDate FROM Configuration (nolock) WHERE Id = @Id";

            using (var conn = new SqlConnection(configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var queryResult = conn.QueryFirstOrDefault<ConfigurationEntity>(query, new
                    {
                        Id = id
                    });

                    result.Operation = queryResult;
                    result.IsSuccess = true;
                    result.Message = "Success";
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                    result.IsSuccess = false;
                }
            }

            return result;
        }

        public OperationResult<bool> Save(ConfigurationEntity item)
        {
            var result = new OperationResult<bool>();

            var query = "INSERT Configuration VALUES(@ApplicationId, @DataType, @Key, @Value, @IsActive, @IsNew, @IsProcessed, @CreatedDate)";

            using (var conn = new SqlConnection(this.configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var queryResult = conn.Execute(query, new
                    {
                        ApplicationId = item.ApplicationId,
                        DataType = item.DataType,
                        Key = item.Key,
                        Value = item.Value,
                        IsActive = item.IsActive,
                        IsNew = item.IsNew,
                        IsProcessed = false,
                        CreatedDate = item.CreatedDate,
                    }) > 0;

                    result.Operation = queryResult;
                    result.IsSuccess = true;
                    result.Message = "Success";
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Message = ex.Message;
                }
            }

            return result;
        }
    }
}
