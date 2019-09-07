using Configuration.DAL.Entity;
using System;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using System.Linq;
using Configuration.BL.Helper;

namespace Configuration.BL.Repository.Application
{
    public class ApplicationRepository : IBaseRepository<ApplicationEntity>
    {
        private IConfigHelper configHelper;

        public ApplicationRepository(IConfigHelper configHelper)
        {
            this.configHelper = configHelper;
        }

        public OperationResult<bool> Delete(int id)
        {
            var result = new OperationResult<bool>();

            var applicationEntity = this.GetOne(id);

            string query = "UPDATE Application SET IsActive = 0 WHERE Id = @Id";

            if (applicationEntity != null)
            {
                using (var conn = new SqlConnection(this.configHelper.ConfigurationServiceDb))
                {
                    try
                    {
                        var queryResult = conn.ExecuteScalar<int>(query, new { Id = id });

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

        public OperationResult<bool> Edit(int id, ApplicationEntity item)
        {
            var result = new OperationResult<bool>();

            var applicationEntity = this.GetOne(id);

            var query = "UPDATE Application SET Name=@Name, Description=@Description, IsActive=@IsActive, CreatedDate=@CreatedDate  WHERE Id = @Id";
            using (var conn = new SqlConnection(this.configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var queryResult = conn.ExecuteScalar<int>(query, new
                    {
                        Name = item.Name,
                        Description = item.Description,
                        IsActive = item.IsActive,
                        CreatedDate = item.CreatedDate,
                        Id = item.Id
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

        public OperationResult<List<ApplicationEntity>> GetActiveItems()
        {
            var resultList = new OperationResult<List<ApplicationEntity>>();
            resultList = new OperationResult<List<ApplicationEntity>>();

            var query = "SELECT Id, Name, Description, IsActive, CreatedDate FROM Application (nolock) WHERE IsActive = 1";

            using (var conn = new SqlConnection(configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var result = conn.Query<ApplicationEntity>(query).OrderBy(n => n.Name).ToList();

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

        public OperationResult<List<ApplicationEntity>> GetItems()
        {
            var resultList = new OperationResult<List<ApplicationEntity>>();
            resultList = new OperationResult<List<ApplicationEntity>>();

            var query = "SELECT Id, Name, Description, IsActive, CreatedDate FROM Application (nolock)";

            using (var conn = new SqlConnection(configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var result = conn.Query<ApplicationEntity>(query).OrderBy(n => n.Name).ToList();

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

        public OperationResult<ApplicationEntity> GetOne(int id)
        {
            var result = new OperationResult<ApplicationEntity>();

            string query = "SELECT TOP 1 Id, Name, Description, IsActive, CreatedDate FROM Application (nolock) WHERE Id = @Id";

            using (var conn = new SqlConnection(configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var queryResult = conn.QueryFirstOrDefault<ApplicationEntity>(query, new
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

        public OperationResult<bool> Save(ApplicationEntity item)
        {
            var result = new OperationResult<bool>();

            var query = "INSERT Application VALUES(@Name, @Description, @IsActive, @CreatedDate)";

            using (var conn = new SqlConnection(this.configHelper.ConfigurationServiceDb))
            {
                try
                {
                    var queryResult = conn.Execute(query, new
                    {
                        Name = item.Name,
                        Description = item.Description,
                        IsActive = false,
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
