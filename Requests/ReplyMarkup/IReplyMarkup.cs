namespace Telebot;

/// <summary>
/// Общий интерфейс для всех вариантов <c>reply_markup</c>:
/// <see cref="InlineKeyboardMarkup"/>, <see cref="ReplyKeyboardMarkup"/>,
/// <see cref="ReplyKeyboardRemove"/>, <see cref="ForceReply"/>.
/// </summary>
/// <remarks>
/// В Bot API <c>reply_markup</c> — union без тега; Telegram различает варианты
/// по «маркерным» полям (<c>inline_keyboard</c>, <c>keyboard</c>, <c>remove_keyboard</c>,
/// <c>force_reply</c>). Каждая реализация сама знает своё JSON-представление.
/// </remarks>
public interface IReplyMarkup
{
    /// <summary>Возвращает JSON-строку для поля формы <c>reply_markup</c>.</summary>
    string ToJson();
}