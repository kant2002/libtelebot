namespace Telebot;

/// <summary>
/// Параметры <c>stopPoll</c> — принудительное закрытие опроса, отправленного ботом.
/// Возвращает финальный <see cref="Telebot.Models.Poll"/> со всей статистикой.
/// </summary>
/// <remarks>
/// Опрос идентифицируется парой <see cref="ChatId"/> + <see cref="MessageId"/>,
/// а не <see cref="Telebot.Models.Poll.Id"/>.
/// </remarks>
/// <param name="ChatId">Идентификатор чата с опросом.</param>
/// <param name="MessageId">Идентификатор сообщения с опросом.</param>
/// <param name="ReplyMarkup">Новая инлайн-клавиатура. <c>null</c> — снять клавиатуру.</param>
public sealed record StopPollRequestParams(
    long ChatId,
    int MessageId,
    InlineKeyboardMarkup? ReplyMarkup = null
) : TelegramRequest("stopPoll")
{
    /// <inheritdoc />
    public override IEnumerable<TelegramRequestField> GetRequestFields()
    {
        yield return new TelegramRequestField("chat_id", ChatId.ToString());
        yield return new TelegramRequestField("message_id", MessageId.ToString());

        if (ReplyMarkup is not null)
            yield return new TelegramRequestField("reply_markup", ReplyMarkup.ToJson());
    }
}