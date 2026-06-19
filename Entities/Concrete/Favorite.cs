using Core.Entities;

namespace Entities.Concrete
{
    public class Favorite : IEntity
    {
        public int Id { get; set; }
        public int NewsId { get; set; }
        public int UserId { get; set; }
    }
}
