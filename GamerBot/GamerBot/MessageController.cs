using GamerBot.BusinessLogic.Services;
using GamerBot.Common.ViewModels;
using GamerBot.Common.ViewModels.UserEditViewModels;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace GamerBot;

public class MessageController
{
    private static UserService _userService;
    
    public static int index;

    public static List<TeammateViewModel> teammates;
    
    public MessageController(UserService userService)
    {
        _userService = userService;
    }
    
        public async Task HandleUpdatesAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message?.Text != null)
            {
                await HadleMessage(botClient, update.Message);
            }
        }

        private static async Task HadleMessage(ITelegramBotClient botClient, Message message)
        {
            if (CheckException(message) != null)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, CheckException(message), replyMarkup: KeyboardMain());
                return;
            }
            
            string userMessage = message.Text.ToLower();

            if (userMessage == "/start")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите команду:", replyMarkup: KeyboardMain());
                return;
            }
            
            if (userMessage == "/keyboard")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выберите команду:", replyMarkup: KeyboardMain());
                return;
            }

            if (userMessage == "end search")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Список моих команд:", replyMarkup: KeyboardMain());
                return;
            }
            
            if (userMessage == "end")
            {
                ReplyKeyboardMarkup keyboardExit = new(new[]
                {
                    new KeyboardButton[] {"/start"},
                })
                {
                    ResizeKeyboard = true
                };
                
                await botClient.SendTextMessageAsync(message.Chat.Id, "Нажмите чтобы начать:", replyMarkup: keyboardExit);
                return;
            }
            
            if (userMessage == "next")
            {
                index++;
                
                NextTeammate(botClient, message);
                
                return;
            }
            
            if (userMessage == "edit account")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Для редактирования профиля, необходимо введите ваш Email, поле которое хотите редактировать " + 
                            "и новое значение поля, по образцу:\n" +
                            "Edit\n" +
                            "email123@gmail.com\n" +
                            "Возраст\n" +
                            "20");
                
                return;
            }
            
            if (userMessage[..4] == "edit")
            {
                string[] messageArray = message.Text.Split('\n');
                List<string> userInfo = messageArray.ToList();
            
                switch (userInfo[2])
                {
                    case "Возраст":
                        var modelAge = new AgeEditViewModel
                        {
                            Email = userInfo[1],
                            Age = Convert.ToInt32(userInfo[3]),
                        };
                        
                        _userService.EditAge(modelAge);
                        break;

                    case "Игра":
                        var modelGame = new GameEditViewModel()
                        {
                            Email = userInfo[1],
                            Game = userInfo[3],
                        };
                        
                        _userService.EditGame(modelGame);
                        break;
                    
                    case "Steam URL":
                        var modelSteam = new SteamEditViewModel
                        {
                            Email = userInfo[1],
                            SteamUrl = userInfo[3],
                        };
                        
                        _userService.EditSteam(modelSteam);
                        break;
                    
                    case "Звание":
                        var modelRank = new RankEditViewModel
                        {
                            Email = userInfo[1],
                            Rank = userInfo[3],
                        };
                        
                        _userService.EditRank(modelRank);
                        break;
                    
                    default: 
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Такое поле не найдено, либо его нельзя изменить");
                        break;
                }
                
                await botClient.SendTextMessageAsync(message.Chat.Id, "Пользователь успешно редактирован.");
            }
            
            if (userMessage == "start search")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите игру и звание для поиска, " + 
                                                                      "по образцу:\n" +
                                                                      "Search\n" +
                                                                      "Игра\n" +
                                                                      "Ранг");
                
                return;
            }

            if (userMessage[..6] == "search")
            {
                string[] messageArray = message.Text.Split('\n');
                List<string> userInfo = messageArray.ToList();
            
                var model = new SearchViewModel()
                {
                    Game = userInfo[1],
                    Rank = userInfo[2]
                };
                
                teammates = _userService.Search(model);

                if (teammates.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, $"Пока в моей базе нет таких игроков :(", 
                        replyMarkup: KeyboardMain());
                }

                index = 0;
                
                NextTeammate(botClient, message);
                
                return;
            }

            if (userMessage == "delete account")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите ключевое слово Удалить и ваш Email для удаления аккаунта, следуйте примеру:\n" + 
                                                                      "Delete\nEmail");
                return;
            }

            if (userMessage[..6] == "delete")
            {
                string[] messageArray = message.Text.Split('\n');
                List<string> userInfo = messageArray.ToList();
            
                var model = new DeleteViewModel()
                {
                    Email = userInfo[1],
                };

                await botClient.SendTextMessageAsync(message.Chat.Id, $"{_userService.Delete(model)}", 
                    replyMarkup: KeyboardMain());
                
                return;
            }
            
            if (userMessage == "register")
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Введите информацию о вас в таком формате:\n" + 
                                                                      "Registration\nEmail\nИмя\nВозраст\nSteam Url\nИгра\nЗвание");
                return;
            }

            if (userMessage[..12] == "registration")
            {
                string[] messageArray = message.Text.Split('\n');
                List<string> userInfo = messageArray.ToList();
            
                var model = new UserCreateViewModel
                {
                    Email = userInfo[1],
                    Name = userInfo[2],
                    Age = Convert.ToInt32(userInfo[3]),
                    SteamUrl = userInfo[4],
                    Game = userInfo[5],
                    Rank = userInfo[6]
                };
            
                _userService.Create(model);
            
                await botClient.SendTextMessageAsync(message.Chat.Id, "Пользователь добавлен успешно.");
            }
            else
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Такой команды у меня нету.");
            }
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Ошибка телеграм АПИ:\n{apiRequestException.ErrorCode}\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private static string CheckException(Message message)
        {
            if (string.IsNullOrWhiteSpace(message.Text))
            {
                return "Такой команды у меня нету.";
            }

            if (message.Text.Length < 3)
            {
                return "Такой команды у меня нету.";
            }
            
            return null;
        }
        
        private static async void NextTeammate(ITelegramBotClient botClient, Message message)
        {
            if (index == teammates.Count)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, $"Больше игроков по таким параметрам у меня нету."
                      , replyMarkup:KeyboardMain());
                
                return;
            }
            
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Ваш тиммейт:\n" +
                       $"{teammates[index].Name}\n{teammates[index].Age}\n{teammates[index].Rank}\n" +
                       $"{teammates[index].SteamUrl}", replyMarkup:KeyboardSearch());
        }
        
        private static ReplyKeyboardMarkup KeyboardSearch()
        {
            ReplyKeyboardMarkup keyboardSearch = new(new[]
            {
                new KeyboardButton[] {"End search", "Next"},
            })
            {
                ResizeKeyboard = true
            };

            return keyboardSearch;
        }
        
        private static ReplyKeyboardMarkup KeyboardMain()
        {
            ReplyKeyboardMarkup keyboardMain = new(new[]
            {
                new KeyboardButton[] {"Register", "Edit account", "Delete account"},
                new KeyboardButton[] {"Start search", "End"},
            })
            {
                ResizeKeyboard = true
            };
            
            return keyboardMain;
        }    
}