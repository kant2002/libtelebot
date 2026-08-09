namespace Telebot;

/// <summary>
/// Параметры <c>getMe</c> — запрос без аргументов, возвращает информацию о боте.
/// </summary>
public sealed record GetMeRequestParams() : TelegramRequest("GetMe"), ITelegramEncodable;