using GamerBot.Common.ViewModels;
using GamerBot.Common.ViewModels.UserEditViewModels;

namespace GamerBot.BusinessLogic.Interfaces;

public interface IUserService
{
    string Create(UserCreateViewModel model);
    List<TeammateViewModel> Search(SearchViewModel model);
    string Delete(DeleteViewModel model);
    void EditAge(AgeEditViewModel model);
    void EditSteam(SteamEditViewModel model);
    void EditGame(GameEditViewModel model);
    void EditRank(RankEditViewModel model);
}