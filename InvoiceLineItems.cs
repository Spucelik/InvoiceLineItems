using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Data;

namespace InvoiceLines
{
    public static class InvoiceLineItems
    {
        [FunctionName("InvoiceLineItems")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string jsonContent = await new StreamReader(req.Body).ReadToEndAsync();
            DataTable dtOutput = new DataTable("JsonOutput");
            char[] charsToTrim = { ' ', '.' };

            try
            {
                if (jsonContent != "")
                {
                    Root tRoot = JsonConvert.DeserializeObject<Root>(jsonContent);

                    foreach (var result in tRoot.analyzeResult.pageResults)
                    {
                        foreach (var objTable in result.tables)
                        {
                            int iColumns = objTable.columns;
                            int iRows = objTable.rows;

                            dtOutput.Columns.Add("RowID",typeof(int));
                            dtOutput.Columns.Add("Qty", typeof(string));
                            dtOutput.Columns.Add("Description", typeof(string));
                            //dtOutput.Columns.Add("Unit", typeof(string));
                            dtOutput.Columns.Add("UnitPrice", typeof(string));
                            dtOutput.Columns.Add("LineTotal", typeof(string));


                            for (int iCol = 0; iCol <= iColumns; iCol++)
                            {
                                DataRow dr = dtOutput.NewRow();
                                dr["RowID"] = iCol;
                                dtOutput.Rows.Add(dr);
                            }

                            foreach (var objCell in objTable.cells)
                            {
                                //Add the column headers
                                if (objCell.rowIndex != 0)
                                {
                                    switch (objCell.columnIndex)
                                    {
                                        case 0:
                                            dtOutput.Rows[objCell.rowIndex]["Qty"] = objCell.text;
                                            //jsonOutputHelper.UnitColumnIndex = objCell.columnIndex;
                                            break;

                                        case 1:
                                            dtOutput.Rows[objCell.rowIndex]["Description"] = objCell.text;
                                            //jsonOutputHelper.QuantityColumnIndex = objCell.columnIndex;
                                            break;
                                        //case 2:
                                        //    dtOutput.Rows[objCell.rowIndex]["Unit"] = objCell.text;
                                            //jsonOutputHelper.QuantityColumnIndex = objCell.columnIndex;
                                            //break;
                                        case 2:
                                            dtOutput.Rows[objCell.rowIndex]["UnitPrice"] = objCell.text;
                                            //jsonOutputHelper.QuantityColumnIndex = objCell.columnIndex;
                                            break;
                                        case 3:
                                            dtOutput.Rows[objCell.rowIndex]["LineTotal"] = objCell.text;
                                            //jsonOutputHelper.QuantityColumnIndex = objCell.columnIndex;
                                            break;
                                    }
                                }
                               
                            }
                            
                        }
                        int iResult = result.page;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(dtOutput,Formatting.Indented);
            
            return (ActionResult)new OkObjectResult(JSONString);

        }
    }
}
