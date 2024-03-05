namespace new_project.Repositary
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Data.SqlClient;
    using new_project.Models;
    using System.Collections.Generic;

    public class Repository
    {
        public class TeacherRepository
        {
            SqlConnection con;
            SqlCommand cmd;
            private object Email;
            private object Phone;
            private object Salary;
            private object Course_Title;


            public TeacherRepository()  //constructor
            {
                con = new SqlConnection("server = SAAHID; database = SAAHID3; integrated security = true; TrustServerCertificate = True");
            }

            public List<Teacher> getAll()
            {
                List<Teacher> data = new List<Teacher>();
                using (con)
                {
                    con.Open();
                    string _query = "select * from Teacher order by Name ";
                    using (SqlCommand cmd = new SqlCommand(_query, con))
                    {
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            data.Add(new Teacher() { 
                                id = Convert.ToInt32(dr["id"]), 
                                Name = dr["Name"].ToString(),
                                Phone = dr["Phone"].ToString(),
                                Course_Title = dr["Course_Title"].ToString(),
                                Email = dr["Email"].ToString(),
                                Salary = dr["Salary"].ToString()
                            }
                            );
                        }
                    }
                }
                return data;
            }

            public Teacher get_by_id(int id)
            {
                Teacher data = new Teacher();
                using (con)
                {
                    con.Open();
                    string _query = $"select * from Teacher where id=@id";
                    using (SqlCommand cmd = new SqlCommand(_query, con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            data = new Teacher() { 
                                id = Convert.ToInt32(dr["id"]), 
                                Name = dr["Name"].ToString(),
                                Phone = dr["Phone"].ToString(),
                                Course_Title = dr["Course_Title"].ToString(),
                                Email = dr["Email"].ToString(),
                                Salary = dr["Salary"].ToString()
                            };
                        }
                    }
                }
                return data;
            }

            public bool create(string name, string email, string phone, string salary, string courseTitle)
            {
                using (con)
                {
                    con.Open();
                    string _query = $"INSERT INTO Teacher(Name, Email, Phone, Salary, Course_Title) VALUES (@Name, @Email, @Phone, @Salary, @Course_Title)";
                    using (SqlCommand cmd = new SqlCommand(_query, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Salary", salary);
                        cmd.Parameters.AddWithValue("@Course_Title", courseTitle);

                        int count = cmd.ExecuteNonQuery();
                        return count > 0;
                    }
                }
            }



            public bool update(int id, string newname, string Email, string Phone, string Salary, string Course_Title)
            {
                using (con)
                {
                    con.Open();
                    string _query = $"update Teacher set name='{newname}',Phone='{Phone}',Email='{Email}',Salary='{Salary}',Course_Title='{Course_Title}' where id={id}";
                    cmd = new SqlCommand(_query, con);

                    int count = cmd.ExecuteNonQuery();
                    return count > 0;
                }
            }

            public bool delete(int id)
            {
                using (con)
                {
                    con.Open();
                    string _query = $"delete from Teacher where id={id}";
                    cmd = new SqlCommand(_query, con);

                    int count = cmd.ExecuteNonQuery();
                    return count > 0;
                }
            }

            //internal void update(string name, string email, string phone, string salary, string course_Title)
            //{
            //    throw new NotImplementedException();
            //}
        }
    }
}
