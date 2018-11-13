using System.Collections.Generic;


namespace WebDev_MainLab.Models
{
    public interface IRepository
    {
        List<Goods> Goods { get; }
        void RemoveItem(int? id);
        void AddItem(Goods item);
        void UpdateItem(Goods item);
        Goods getByID(int? id);
        List<Commentar> Comments { get; }
        void AddComments(Commentar comment);
        int DeleteComments(int? id);
        ApplicationUser getUser(string userName);

    }
}
