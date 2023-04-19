using System;

public class Repository
{
    private SqlConnection con;
    private void connection()   //To Handle connection related activities    
    {
        string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
        con = new SqlConnection(constr);

    }
}
