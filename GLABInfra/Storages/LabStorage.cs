using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using GLAB.domain.Storages;
using Microsoft.Extensions.Configuration;


namespace GLAB.infra.Storages
{
    public class LabStorage : ILabStorage
    {
        private string connectionString;

        public LabStorage(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("db_aa5c49_rachediradouane");
        }

        public async  Task deleteLaboratory(string id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            string updateCommandText = @"
        UPDATE dbo.LABORATORY 
        SET Status = -1 
        WHERE Id = @aLaboratoryId";

            SqlCommand cmd = new SqlCommand(updateCommandText, connection);

            cmd.Parameters.AddWithValue("@aLaboratoryId", id);

            await cmd.ExecuteNonQueryAsync();

        }

        public async Task insertLaboratory(Laboratory laboratory)
        {
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("Insert dbo.LABORATOIRES(LaboratoryId,Name, Adress,University, Faculty, Department,Acronyme,PhoneNumber,Email,AgrementNumber,CreationDate,Logo,WebSite) " +
                                 "VALUES(@aLaboratoryId,@aName, @aAdress,@aUniversity,@aFaculty, @aDepartment,@aAcronyme,@aPhoneNumber,@aEmail,@aAgrementNumber,@aCreationDate,@aLogo,@aWebSite)", connection);

            cmd.Parameters.AddWithValue("@aLaboratoryId", laboratory.LaboratoryId);
            cmd.Parameters.AddWithValue("@aName", laboratory.Name);
            cmd.Parameters.AddWithValue("@aAdress", laboratory.Adresse);
            cmd.Parameters.AddWithValue("@aUniversity", laboratory.University);
            cmd.Parameters.AddWithValue("@aDepartment", laboratory.Departement);
            cmd.Parameters.AddWithValue("@aAcronyme", laboratory.Acronyme);
            cmd.Parameters.AddWithValue("@aPhoneNumber", laboratory.PhoneNumber);
            cmd.Parameters.AddWithValue("@aEmail", laboratory.Email);
            cmd.Parameters.AddWithValue("@aAgrementNumber", laboratory.NumAgrement);
            cmd.Parameters.AddWithValue("@aCreationDate", laboratory.CreationDate);
            cmd.Parameters.AddWithValue("@aLogo", laboratory.Logo);
            cmd.Parameters.AddWithValue("@aWebSite", laboratory.WebSite);


            connection.Open();
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Laboratory>> SelectLaboratories()
        {
            List<Laboratory> laboratories= new List<Laboratory>();
            await using var connection = new SqlConnection(connectionString);
            SqlCommand cmd = new("select * from dbo.LABORATORY", connection);

            DataTable ds = new();
            SqlDataAdapter da = new(cmd);

            connection.Open();
            da.Fill(ds);

            foreach (DataRow row in ds.Rows)
            {
                Laboratory labo = getLaboratoireFromDataRow(row);
                laboratories.Add(labo);
            }

            return laboratories;
        }

        public async  Task updateLaboratory(Laboratory laboratory)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            string updateCommandText = @"
        UPDATE dbo.LABORATORY 
        SET Nom = @aName, 
            Adresse = @aAdresse,
            Universite = @aUniversity,
            Faculte = @aFaculty,
            Departement = @aDepartement,
            Acronyme = @aAcronyme,
            PhoneNumber = @aPhoneNumber,
            Email = @aEmail,
            NumAgrement = @aNumAgrement,
            CreationDate = @aCreationDate,
            Logo = @aLogo,
            WebSite = @aWebSite
        WHERE Id = @aLaboratoryId";

            SqlCommand cmd = new SqlCommand(updateCommandText, connection);

            cmd.Parameters.AddWithValue("@aName", laboratory.Name);
            cmd.Parameters.AddWithValue("@aAdresse", laboratory.Adresse);
            cmd.Parameters.AddWithValue("@aUniversity", laboratory.University);
            cmd.Parameters.AddWithValue("@aFaculty", laboratory.Faculty);
            cmd.Parameters.AddWithValue("@aDepartement", laboratory.Departement);
            cmd.Parameters.AddWithValue("@aAcronyme", laboratory.Acronyme);
            cmd.Parameters.AddWithValue("@aPhoneNumber", laboratory.PhoneNumber);
            cmd.Parameters.AddWithValue("@aEmail", laboratory.Email);
            cmd.Parameters.AddWithValue("@aNumAgrement", laboratory.NumAgrement);
            cmd.Parameters.AddWithValue("@aCreationDate", laboratory.CreationDate);
            cmd.Parameters.AddWithValue("@aLogo", laboratory.Logo);
            cmd.Parameters.AddWithValue("@aWebSite", laboratory.WebSite);
            cmd.Parameters.AddWithValue("@aLaboratoryId", laboratory.LaboratoryId);

            await cmd.ExecuteNonQueryAsync();
        }
        private static Laboratory getLaboratoireFromDataRow(DataRow row)
        {
            return new()
            {
                LaboratoryId = (string)row["LaboratoryId"],
                Name = (string)row["Name"],
                Adresse = (string)row["Adress"],
                University = (string)row["University"],
                Faculty = (string)row["Faculty"],
                Departement = (string)row["Departement"],
                Acronyme = (string)row["Acronyme"],
                PhoneNumber = (string)row["PhoneNumber"],
                Email = (string)row["Email"],
                NumAgrement = (string)row["AgrementNumber"],
                CreationDate = (DateTime)row["CreationDate"],
                Logo = (byte[])row["Logo"],
                WebSite = (string)row["WebSite"],
            };
        }

    }
}
