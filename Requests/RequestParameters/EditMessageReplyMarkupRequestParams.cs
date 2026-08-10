namespace Telebot;

/// <summary>
/// Параметры <c>editMessageReplyMarkup</c> — замена или снятие инлайн-клавиатуры
/// без изменения текста сообщения.
/// </summary>
/// <param name="ChatId">Идентификатор чата с редактируемым сообщением.</param>
/// <param name="MessageId">Идентификатор редактируемого сообщения.</param>
/// <param name="ReplyMarkup">Новая инлайн-клавиатура. <c>null</c> — снять клавиатуру.</param>
public sealed record EditMessageReplyMarkupRequestParams(
    long ChatId,
    int MessageId,
    InlineKeyboardMarkup? ReplyMarkup = null
) : TelegramRequest("editMessageReplyMarkup")
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