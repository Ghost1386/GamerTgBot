using GamerBot.Common.ViewModels;
using GamerBot.Common.ViewModels.UserEditViewModels;

namespace GamerBot.BusinessLogic.Interfaces;

public interface IUserService
{
    public string Create(UserCreateViewModel model);
    public List<TeammateViewModel> Search(SearchViewModel model);
    public string Delete(DeleteViewModel model);
    public void EditAge(AgeEditViewModel model);
    public void EditSteam(SteamEditViewModel model);
    public void EditGame(GameEditViewModel model);
    public void EditRank(RankEditViewModel model);
}