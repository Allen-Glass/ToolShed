using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Toolshed.Models.Tools;

namespace ToolShed.Repository.Interfaces
{
    public interface IToolSQLService
    {
        /// <summary>
        /// Saving a tool to sql
        /// </summary>
        /// <param name="tool">user submitted tool object</param>
        Task StoreToolAsync(Tool tool);
    }
}
