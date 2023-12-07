using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using LitJson;


public class user_info
{
    public string User_name { get; private set; }
    public string User_Password { get; private set; }
    public string User_ID { get; private set; }

    public user_info(string id, string password ,string name)
    {
        User_ID = id;
        User_Password = password;
        User_name = name;
    }
}

public class server_info
{
    /*
     string serverInfo =
            $"server = {itemdata[0]["IP"]};" + $"" +
            $" Database = {itemdata[0]["TableName"]};" +
            $" Uid = {itemdata[0]["ID"]};" +
            $" Pwd = {itemdata[0]["PW"]};" +
            $" Port = {itemdata[0]["PORT"]};" +
            $" CharSet=utf8;";
    */
    public string IP { get; private set; }
    public string TableName { get; private set; }
    public string ID { get; private set; }
    public string PW { get; private set; }
    public string PORT { get; private set; }

    public server_info(string ip, string tableName, string id, string pw, string port)
    {
        IP = ip;
        TableName = tableName;
        ID = id;
        PW = pw;
        PORT = port;
    }
}


public class SQLManager : MonoBehaviour
{
    public user_info info;

    public MySqlConnection connection;
    public MySqlDataReader reader;

    public string DB_path = string.Empty;
    public static SQLManager instance = null;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        DB_path = Application.dataPath + "/Database";
        string serverinfo = Serverset(DB_path);
        try
        {
            if(serverinfo.Equals(string.Empty))
            {
                Debug.Log("SQL Server Json Error!");
                return;
            }

            connection = new MySqlConnection(serverinfo);
            connection.Open();
            Debug.Log("SQL Server Open Compelate!");

        }catch(Exception e)
        {
            Debug.Log(e.Message) ;
        }
    }

    private void Default_Data(string path)
    {

        List<server_info> userInfo = new List<server_info>();
        userInfo.Add(new server_info("13.124.124.144", "programming", "root", "1234", "3306"));

        JsonData data = JsonMapper.ToJson(userInfo);
        File.WriteAllText(path + "/config.json", data.ToString());
    }

    private string Serverset(string path)
    {
        if (!File.Exists(path)) // ��ΰ� �ֳ���?
        {
            Directory.CreateDirectory(path);
        }

        if (!File.Exists(path + "/config.json"))  // ������ �ֳ���?
        {
            Default_Data(path);
        }
        
        string Jsonstring = File.ReadAllText(path + "/config.json");

        JsonData itemdata = JsonMapper.ToObject(Jsonstring);
        string serverInfo =
            $"server = {itemdata[0]["IP"]};" + $"" +
            $" Database = {itemdata[0]["TableName"]};" +
            $" Uid = {itemdata[0]["ID"]};" +
            $" Pwd = {itemdata[0]["PW"]};" +
            $" Port = {itemdata[0]["PORT"]};" +
            $" CharSet=utf8;";

        return serverInfo;
            
    }

    private bool connection_check(MySqlConnection con)
    {
        //���� MySQLConnection open �� �ƴ϶��?
        if(con.State != System.Data.ConnectionState.Open)
        {
            con.Open();
            if(con.State != System.Data.ConnectionState.Open)
            {
                return false;
            }
        }
       
            return true;
       
    }
   public bool Join(string id , string password , string nickname)
    {
        try
        {
            //1.connection open ��Ȳ���� Ȯ�� -> �޼ҵ�ȭ
            if (!connection_check(connection))
            {
                return false;
            }
           
            if (IsIdOrNicknameDuplicate(id, nickname))
            {
                // �ߺ��� ���̵� �Ǵ� �г����� ������ ���� ����
                return false;
            }

            string SQL_command =
                string.Format(@"INSERT INTO user_info VALUE('{0}','{1}','{2}');", id, password ,nickname);

            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Debug.Log("ȸ������ ����!");
                return true;
            }
            else
            {
                Debug.Log("ȸ������ ����.");
                return false;
            }
        }
        
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return false;
        }
    }
    private bool IsIdOrNicknameDuplicate(string id, string nickname)
    {
        // ���̵� �Ǵ� �г����� �̹� �����ϴ��� Ȯ���ϴ� ����
        string duplicateCheckCommand =
            string.Format(@"SELECT COUNT(*) FROM user_info WHERE User_ID = '{0}' OR User_Name = '{1}';", id, nickname);

        MySqlCommand checkCmd = new MySqlCommand(duplicateCheckCommand, connection);
        int count = Convert.ToInt32(checkCmd.ExecuteScalar());

        // count�� 0�� �ƴϸ� �ߺ��� ���̵� �Ǵ� �г����� ������
        if(count > 0)
        {
            Debug.Log("�ߺ��߻�");
            
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool Login(string id, string password)
    {
        //���������� DB���� �����͸� ������ ���� �޼ҵ�
        //��ȸ�Ǵ� �����Ͱ� ���ٸ� bool���� false;
        //��ȸ�� �Ǵ� �����Ͱ� �ִٸ� true�� ����������
        //������ ������ info ���ٰ� ���� ������ ����.

        //������ ��������� 

        /*
         1.connection open ��Ȳ���� Ȯ�� -> �޼ҵ�ȭ

        
        2. Reader ���°� �а� �ִ� ��Ȳ���� Ȯ�� (�� �������� ���� �Ѱ���)
        3. �����͸� �� �о����� Reader�� ���� Ȯ�� �� Close
         */

        try
        {
            //1.connection open ��Ȳ���� Ȯ�� -> �޼ҵ�ȭ
            if (!connection_check(connection))
            {
                return false;
            }

            string SQL_command =
                string.Format(@"SELECT User_ID,User_Password,User_Name FROM user_info
                              WHERE User_ID='{0}' AND User_Password = '{1}' ;", id, password);

            MySqlCommand cmd = new MySqlCommand(SQL_command, connection);
            reader = cmd.ExecuteReader();

            //Reader ���� �����Ͱ� 1�� �̻� ������?
            if (reader.HasRows)
            {
                //���� �����͸� �ϳ��� ������
                while (reader.Read())
                {
                    /*
                     ���׿�����
                     */
                    string ID = (reader.IsDBNull(0)) ? string.Empty : (string)reader["User_ID"].ToString();
                    string pass = (reader.IsDBNull(1)) ? string.Empty : (string)reader["User_Password"].ToString();
                    string nick = (reader.IsDBNull(2)) ? string.Empty : (string)reader["User_Name"].ToString();
                    if(!ID.Equals(string.Empty) || !pass.Equals(string.Empty))
                    {
                        //���������� Data�� �ҷ��� ��Ȳ
                        info = new user_info(ID, pass ,nick);
                        if (!reader.IsClosed) reader.Close();
                        return true;
                    }
                    else//�α��ν���
                    {
                        break;
                    }
                }//while
            }//if
                if (!reader.IsClosed) reader.Close();
            return false;


        }catch(Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return false;
        }
    }
}
