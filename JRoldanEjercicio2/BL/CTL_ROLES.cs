using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace BL
{
    public class CTL_ROLES
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string Query = "CTL_ROLESGetAll";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = Query;
                        cmd.Connection = context;
                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tableRol = new DataTable();
                        da.Fill(tableRol);
                        if (tableRol.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableRol.Rows)
                            {
                                ML.CTL_ROLES rol = new ML.CTL_ROLES();

                                rol.IdRole = int.Parse(row[0].ToString());
                                rol.RoleName = row[1].ToString();
                               



                                result.Objects.Add(rol);

                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No hay registros";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }
    }
}
