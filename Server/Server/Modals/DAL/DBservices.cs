using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Models;
//using RuppinProj.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
/// 
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    //--------------------------------------------------------------------------------------------------
    // This method is a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {
        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // Read Objects
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command Read a user to the users table and close connection
    public List<Object> GetAvgPricePerNight(int month)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadObjects("spAvgPricePerNight", con , month);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<Object> list = new List<Object>();

            while (dataReader.Read())
            {
                list.Add(new
                {
                    city = dataReader["city"].ToString(),
                    AvgPricePerNight = Convert.ToDouble(dataReader["AvgPricePerNight"])
            });
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureReadObjects(String spName, SqlConnection con , int month)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@month", month);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // Update User
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command update a user to the users table and close connection
    public int UpdateUser(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        // create the connection
        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        // call method to create the command
        cmd = CreateCommandWithStoredProcedureUpdateUser("spUpdateUser_L", con, user);

        try
        {
            //Execute the command
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;

            //if (numEffected != 0)
            //{
            //    return true;
            //}
            //else
            //    return false;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the DB connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureUpdateUser(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@lastName", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@isActive", user.IsActive);
        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // Insert User
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command Insert a user to the users table and close connection
    public int InsertUser(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        // create the connection
        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        // call method to create the command
        cmd = CreateCommandWithStoredProcedureInsertUser("spInsertUser_L", con, user);

        try
        {
            //Execute the command
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the DB connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureInsertUser(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);
        cmd.Parameters.AddWithValue("@lastName", user.LastName);
        cmd.Parameters.AddWithValue("@email", user.Email);
        cmd.Parameters.AddWithValue("@password", user.Password);
        cmd.Parameters.AddWithValue("@isActive", user.IsActive);
        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // Read Users
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command Read a user to the users table and close connection
    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadUser("spReadUser_L", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<User> list = new List<User>();

            while (dataReader.Read())
            {
                User user = new User();
                user.FirstName = dataReader["firstName"].ToString();
                user.LastName = dataReader["lastName"].ToString();
                user.Email = dataReader["email"].ToString();
                user.Password = dataReader["password"].ToString();
                user.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                user.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
                list.Add(user);
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureReadUser(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // Delete User
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command update a user to the users table and close connection
    public int DeleteUser(User user)
    {
        SqlConnection con;
        SqlCommand cmd;

        // create the connection
        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        // call method to create the command
        cmd = CreateCommandWithStoredProcedureDeleteUser("spDeleteUser_L", con, user);

        try
        {
            //Execute the command
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;

        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the DB connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureDeleteUser(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@email", user.Email);

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // Insert Flat
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command Insert a user to the users table and close connection
    public int InsertFlat(Flat flat)
    {
        SqlConnection con;
        SqlCommand cmd;

        // create the connection
        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        // call method to create the command
        cmd = CreateCommandWithStoredProcedureInsertFlat("spInsertFlat_L", con, flat);

        try
        {
            //Execute the command
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the DB connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureInsertFlat(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@city", flat.City);
        cmd.Parameters.AddWithValue("@address", flat.Address);
        cmd.Parameters.AddWithValue("@price", flat.Price);
        cmd.Parameters.AddWithValue("@numberOfRooms", flat.NumberOfRooms);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // Read Flats
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command Read a user to the users table and close connection
    public List<Flat> ReadFlats()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadFlats("spReadFlats_L", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<Flat> list = new List<Flat>();

            while (dataReader.Read())
            {
                Flat flat = new Flat();
                flat.Id = Convert.ToInt32(dataReader["id"]);
                flat.City = dataReader["city"].ToString();
                flat.Address = dataReader["address"].ToString();
                flat.Price = Convert.ToDouble(dataReader["price"]);
                flat.NumberOfRooms = Convert.ToInt32(dataReader["numberOfRooms"]);
                list.Add(flat);
            }
            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureReadFlats(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        return cmd;
    }



    //--------------------------------------------------------------------------------------------------
    // Insert Vacation
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command Insert a user to the users table and close connection
    public int InsertVacation(Vacation vacation)
    {
        SqlConnection con;
        SqlCommand cmd;

        // create the connection
        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        // call method to create the command
        cmd = CreateCommandWithStoredProcedureInsertVacation("spInsertVacation_L", con, vacation);

        try
        {
            //Execute the command
            int numEffected = cmd.ExecuteNonQuery();
            return numEffected;
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the DB connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureInsertVacation(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@userId", vacation.UserId);
        cmd.Parameters.AddWithValue("@flatId", vacation.FlatId);
        cmd.Parameters.AddWithValue("@startDate", vacation.StartDate);
        cmd.Parameters.AddWithValue("@endDate", vacation.EndDate);

        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // Read Vacations
    //--------------------------------------------------------------------------------------------------

    //connects, Creates and executes command Read a user to the users table and close connection
    public List<Vacation> ReadVacations()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateCommandWithStoredProcedureReadVacations("spReadVacation_L", con);

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            List<Vacation> list = new List<Vacation>();

            while (dataReader.Read())
            {
                Vacation vacation = new Vacation();
                vacation.Id = Convert.ToInt32(dataReader["id"]);
                vacation.UserId = dataReader["userId"].ToString();
                vacation.FlatId = Convert.ToInt32(dataReader["flatId"]);
                vacation.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                vacation.EndDate = Convert.ToDateTime(dataReader["EndDate"]);
                list.Add(vacation);
            }

            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // Create the SqlCommand using a stored procedure- stored procedure
    private SqlCommand CreateCommandWithStoredProcedureReadVacations(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand();

        cmd.Connection = con;

        cmd.CommandText = spName;

        cmd.CommandTimeout = 10;

        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        return cmd;
    }

    //--------------------------------------------------------------------------------------------------

}