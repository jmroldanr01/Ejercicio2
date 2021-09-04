using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;  

namespace BL
{
    public class CTL_USERS
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
                        string Query = "CTL_USERSGetALL";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = Query;
                        cmd.Connection = context;
                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tableUsuario = new DataTable();
                        da.Fill(tableUsuario);
                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.CTL_USERS usuario = new ML.CTL_USERS();

                                usuario.Id = int.Parse(row[0].ToString());
                                usuario.Name = row[1].ToString();
                                usuario.LastName = row[2].ToString();
                                usuario.SurName = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                byte[] contraseña =(byte[])row[6];
                                usuario.Password = Encoding.UTF8.GetString(contraseña,0,contraseña.Length);
                                usuario.Parent = int.Parse(row[7].ToString());
                                usuario.Status = int.Parse(row[8].ToString());
                                usuario.Role = new ML.CTL_ROLES();
                                usuario.Role.IdRole = int.Parse(row[9].ToString());
                                usuario.Role.RoleName = row[10].ToString();



                                result.Objects.Add(usuario);

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
        public static ML.Result Add(ML.CTL_USERS usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string query = "CTL_USERSAdd".ToString();
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = query;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[9];
                        collection[0] = new SqlParameter("IdRole", SqlDbType.Int);
                        collection[0].Value = usuario.Role.IdRole;

                        collection[1] = new SqlParameter("Name", SqlDbType.VarChar);
                        collection[1].Value = usuario.Name;

                        collection[2] = new SqlParameter("LastName", SqlDbType.VarChar);
                        collection[2].Value = usuario.LastName;

                        collection[3] = new SqlParameter("SurName", SqlDbType.VarChar);
                        collection[3].Value = usuario.SurName;

                        collection[4] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[4].Value = usuario.Email;

                        collection[5] = new SqlParameter("UserName", SqlDbType.VarChar);
                        collection[5].Value = usuario.UserName;

                        byte[] contraseña = Encoding.UTF8.GetBytes(usuario.Password);
                        collection[6] = new SqlParameter("Password", SqlDbType.VarBinary);
                        collection[6].Value = contraseña;

                        collection[7] = new SqlParameter("Parent", SqlDbType.Int);
                        collection[7].Value = usuario.Parent;

                        collection[8] = new SqlParameter("Status", SqlDbType.Int);
                        collection[8].Value = usuario.Status;



                        cmd.Parameters.AddRange(collection);

                        context.Open();
                        var resultSP = cmd.ExecuteScalar().ToString();

                        if (resultSP == "Usuario Insertado")
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            if (resultSP.Length >= 49 && resultSP.Substring(0, 9) == "Violation")
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Ya existe el email";
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se Inserto";
                            }

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
        public static ML.Result Update(ML.CTL_USERS usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string query = "CTL_USERSUpdate";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = query;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[10];

                        collection[0] = new SqlParameter("Id", SqlDbType.Int);
                        collection[0].Value = usuario.Id;

                        collection[1] = new SqlParameter("IdRole", SqlDbType.Int);
                        collection[1].Value = usuario.Role.IdRole;

                        collection[2] = new SqlParameter("Name", SqlDbType.VarChar);
                        collection[2].Value = usuario.Name;

                        collection[3] = new SqlParameter("LastName", SqlDbType.VarChar);
                        collection[3].Value = usuario.LastName;

                        collection[4] = new SqlParameter("SurName", SqlDbType.VarChar);
                        collection[4].Value = usuario.SurName;

                        collection[5] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[5].Value = usuario.Email;

                        collection[6] = new SqlParameter("UserName", SqlDbType.VarChar);
                        collection[6].Value = usuario.UserName;

                        byte[] contraseña = Encoding.UTF8.GetBytes(usuario.Password);
                        collection[7] = new SqlParameter("Password", SqlDbType.VarBinary);
                        collection[7].Value = contraseña;

                        collection[8] = new SqlParameter("Parent", SqlDbType.Int);
                        collection[8].Value = usuario.Parent;

                        collection[9] = new SqlParameter("Status", SqlDbType.Int);
                        collection[9].Value = usuario.Status;


                        
                        cmd.Parameters.AddRange(collection);

                        context.Open();
                        var resultSP = cmd.ExecuteScalar().ToString();

                        if (resultSP == "Usuario Actualizado")
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            if (resultSP.Length >= 49 && resultSP.Substring(0, 9) == "Violation")
                            {
                                result.Correct = false;
                                result.ErrorMessage = "Ya existe el email";
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se Inserto";
                            }

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

        public static ML.Result Delete(ML.CTL_USERS usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string Query = "CTL_USERSDelete";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = Query;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("Id", SqlDbType.Int);
                        collection[0].Value = usuario.Id;

                     



                        cmd.Parameters.AddRange(collection);

                        context.Open();

                        int RowsAffected = cmd.ExecuteNonQuery();

                        if (RowsAffected > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al Eliminar del Usuario";
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

        public static ML.Result GetById(int Id)
        {
            ML.Result result = new ML.Result();
            

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string Query = "CTL_USERSGetById";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = Query;
                        cmd.Connection = context;

                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("Id", SqlDbType.Int);
                        collection[0].Value = Id;





                        cmd.Parameters.AddRange(collection);

                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tableUsuario = new DataTable();
                        da.Fill(tableUsuario);
                        if (tableUsuario.Rows.Count > 0)
                        {
                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.CTL_USERS usuario = new ML.CTL_USERS();

                                usuario.Id = int.Parse(row[0].ToString());
                                usuario.Name = row[1].ToString();
                                usuario.LastName = row[2].ToString();
                                usuario.SurName = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                byte[] contraseña = (byte[])row[6];
                                usuario.Password = Encoding.UTF8.GetString(contraseña, 0, contraseña.Length);
                                usuario.Parent = int.Parse(row[7].ToString());
                                usuario.Status = int.Parse(row[8].ToString());
                                usuario.Role = new ML.CTL_ROLES();
                                usuario.Role.IdRole = int.Parse(row[9].ToString());
                                usuario.Role.RoleName = row[10].ToString();



                                result.Object=usuario;

                            }
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No hay registro";
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
        public static ML.Result GetAllBusqueda(ML.CTL_USERS usuarioBusqueda)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string Query = "CTL_USERSGetAllBusqueda";
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.CommandText = Query;
                        cmd.Connection = context;
                        SqlParameter[] collection = new SqlParameter[4];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = usuarioBusqueda.Name;

                        collection[1] = new SqlParameter("ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = usuarioBusqueda.LastName;

                        collection[2] = new SqlParameter("ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = usuarioBusqueda.SurName;

                        collection[3] = new SqlParameter("Email", SqlDbType.VarChar);
                        collection[3].Value = usuarioBusqueda.Email;






                        cmd.Parameters.AddRange(collection);

                        context.Open();

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable tableUsuario = new DataTable();
                        da.Fill(tableUsuario);
                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();
                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.CTL_USERS usuario = new ML.CTL_USERS();

                                usuario.Id = int.Parse(row[0].ToString());
                                usuario.Name = row[1].ToString();
                                usuario.LastName = row[2].ToString();
                                usuario.SurName = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.UserName = row[5].ToString();
                                byte[] contraseña = (byte[])row[6];
                                usuario.Password = Encoding.UTF8.GetString(contraseña, 0, contraseña.Length);
                                usuario.Parent = int.Parse(row[7].ToString());
                                usuario.Status = int.Parse(row[8].ToString());
                                usuario.Role = new ML.CTL_ROLES();
                                usuario.Role.IdRole = int.Parse(row[9].ToString());
                                usuario.Role.RoleName = row[10].ToString();



                                result.Objects.Add(usuario);

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
