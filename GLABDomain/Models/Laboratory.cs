namespace GLAB.domain.Storages
{
    public class Laboratory
    {
        public string LaboratoryId { get; set; }
        public string Name { get; set; }
        public string Adresse { get; set; }
        public string University { get; set; } = "932033522";
        public string Faculty { get; set; }
        public string Departement { get; set; }
        public string Acronyme { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NumAgrement { get; set; }
        public DateTime CreationDate { get; set; }
        public byte[] Logo { get; set; } = new byte[] { };

        public string WebSite { get; set; }

    }
}
