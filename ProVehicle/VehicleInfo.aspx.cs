using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ProVehicle
{
    public partial class VehicleInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void UploadCSV(object sender, EventArgs e)
        {
            string csvPath=Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);
            CultureInfo provider = CultureInfo.InvariantCulture;
            DataTable dt = new DataTable();
            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            int count = 0;

            GridView1.DataSource = dt;
            GridView1.DataBind();

            try
            {
                string csvData = File.ReadAllText(csvPath);

                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        String[] sCells = CSVParser.Split(row);
                        for (int j = 0; j < sCells.Length; j++)
                        {
                            sCells[j] = sCells[j].TrimStart(' ', '"');
                            sCells[j] = sCells[j].TrimEnd('"');
                        }

                        if (count == 0)
                        {
                            foreach (string cell in sCells)
                            {
                                if (count == 0)
                                {
                                    dt.Columns.Add(cell, typeof(Int32));
                                }
                                else if (count == 5)
                                {
                                    dt.Columns.Add(cell, typeof(DateTime));
                                }
                                else
                                {
                                    dt.Columns.Add(cell);
                                }
                                count++;
                            }                            
                        }
                        else
                        {
                            dt.Rows.Add();
                            int i = 0;
                            foreach (string cell in sCells)
                            {
                                if (i == 0)
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = Int32.Parse(cell);
                                }
                                else if (i == 4)
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = "CAD $" + cell;
                                }
                                else if(i==5)
                                {
                                    DateTime dtt = DateTime.Parse(cell);                                
                                    dt.Rows[dt.Rows.Count - 1][i] = dtt.ToShortDateString();
                                }
                                else
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                                }
                                i++;
                            }
                        }
                    }
                }

                //Bind the DataTable.  
                GridView1.DataSource = dt;
                GridView1.DataBind();

                //Count Most valued Vehicle
                var result = dt.AsEnumerable()
                   .GroupBy(r => r.Field<string>("Vehicle"))
                   .OrderBy(r => r.Count())
                   .Select(r => new
                   {
                       Str = r.Key,
                       Count = r.Count()
                   });

                foreach (var item in result)
                {
                    lblPopularVehicle.Text = ($"Most valued Vehicle is <b>{item.Str}</b> : Sold <b>{item.Count}</b> times.");
                }
            }
            catch(Exception ex)
            {
                lblPopularVehicle.Text = "Please upload a valid CSV file.";
            }
            finally
            {
                dt = null;
            }
        }
    }
}