namespace MyCar_Creation.Model
{
    public abstract class BaseModel
    {
        protected BaseModel()
        {
            Id = Guid.NewGuid();
            CreadtedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreadtedDate { get; set; }
    }
}
