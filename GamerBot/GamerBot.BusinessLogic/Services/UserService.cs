using AutoMapper;
using GamerBot.BusinessLogic.Interfaces;
using GamerBot.Common.ViewModels;
using GamerBot.Common.ViewModels.UserEditViewModels;
using GamerBot.Model;
using GamerBot.Model.Models;

namespace GamerBot.BusinessLogic.Services;

public class UserService : IUserService
{
    private readonly ApplicationContext _context;
    private readonly IMapper _mapper;
    
    public UserService(ApplicationContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public string Create(UserCreateViewModel model)
    {
        if (_context.Users.Any(x => x.Email == model.Email))
        {
            return $"Пользователь {model.Email} уже зарегистрирован.";
        }

        var user = _mapper.Map<UserCreateViewModel, User>(model);

        _context.Users.Add(user);
        _context.SaveChanges();
        return "Пользователь добавлен успешно.";
    }

    public List<TeammateViewModel> Search(SearchViewModel model)
    {
        IQueryable<User> query = _context.Users.AsQueryable();

        query = query.Where(x => x.Game == model.Game);
        query = query.Where(x => x.Rank == model.Rank);
        query = query.Where(x => x.ChatId != model.ChatId);

        List<User> users = query.ToList();

        if (users.Count == 0)
        {
            return new List<TeammateViewModel>();
        }
        
        var teammates = new List<TeammateViewModel>();
        
        for (int i = 0; i < users.Count; i++)
        {
            teammates.Add(_mapper.Map<User, TeammateViewModel>(users[i]));
        }
        
        return teammates;
    }

    public string Delete(DeleteViewModel model)
    {
        var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);
        
        if (user == null)
        {
            return "Пользователь не найден.";
        }
        
        _context.Users.Remove(user);

        return "Пользователь успешно удалён";
    }

    public void EditAge(AgeEditViewModel model)
    {
        User user = Get(model.Email);
        
        if (user == null)
        {
            return;
        }

        user.Age = model.Age;

        SaveUpdate(user);
    }

    public void EditSteam(SteamEditViewModel model)
    {
        User user = Get(model.Email);
        
        if (user == null)
        {
            return;
        }
        
        user.SteamUrl = model.SteamUrl;
        
        SaveUpdate(user);
    }

    public void EditGame(GameEditViewModel model)
    {
        User user = Get(model.Email);
        
        if (user == null)
        {
            return;
        }
        
        user.Game = model.Game;
        
        SaveUpdate(user);
    }

    public void EditRank(RankEditViewModel model)
    {
        User user = Get(model.Email);
        
        if (user == null)
        {
            return;
        }
        
        user.Rank = model.Rank;
        
        SaveUpdate(user);
    }

    private User Get(string email)
    {
        var user = _context.Users.SingleOrDefault(x => x.Email == email);
        
        if (user == null)
        {
            return null;
        }
        
        return user;
    }

    private void SaveUpdate(User updatedUser)
    {
        _context.Users.Update(updatedUser);
        _context.SaveChanges();
    }
}