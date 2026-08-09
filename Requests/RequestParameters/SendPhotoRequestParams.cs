namespace Telebot;

/// <summary>
/// Параметры <c>sendPhoto</c> — отправка фотографии.
/// </summary>
/// <param name="ChatId">Идентификатор чата-получателя.</param>
/// <param name="Photo">
/// Источник фото: <see cref="InputFileWithId"/>, <see cref="InputFileWithUrl"/>
/// или <see cref="InputFileWithStream"/>. При потоке транспорт использует multipart.
/// </param>
/// <param name="Caption">Подпись, до 1024 символов.</param>
/// <param name="ParseMode">Режим разбора разметки в <see cref="Caption"/>.</param>
/// <param name="HasSpoiler">Эффект «спойлер» на изображении.</param>
/// <param name="DisableNotification">Отправка без звука.</param>
/// <param name="ProtectContent">Запрет пересылки и сохранения.</param>
/// <param name="ReplyToMessageId">Идентификатор сообщения-цели ответа.</param>
/// <param name="AllowSendingWithoutReply">Отправлять, даже если цель ответа удалена.</param>
public sealed record SendPhotoRequestParams(
    long ChatId,
    InputFile Photo,
    string? Caption = null,
    string? ParseMode = null,
    bool? HasSpoiler = null,
    bool? DisableNotification = null,
    bool? ProtectContent = null,
    int? ReplyToMessageId = null,
    bool? AllowSendingWithoutReply = null
) : TelegramRequest("sendPhoto")
{
    /// <inheritdoc />
    public override IEnumerable<TelegramRequestField> GetRequestFields()
    {
        yield return new TelegramRequestField("chat_id", ChatId.ToString());

        if (Caption is not null)
            yield return new TelegramRequestField("caption", Caption);

        if (ParseMode is not null)
            yield return new TelegramRequestField("parse_mode", ParseMode);

        if (HasSpoiler is not null)
            yield return new TelegramRequestField(
                "has_spoiler",
                HasSpoiler.Value ? "true" : "false"
            );

        if (DisableNotification is not null)
            yield return new TelegramRequestField(
                "disable_notification",
                DisableNotification.Value ? "true" : "false"
            );

        if (ProtectContent is not null)
            yield return new TelegramRequestField(
                "protect_content",
                ProtectContent.Value ? "true" : "false"
            );

        if (ReplyToMessageId is not null)
            yield return new TelegramRequestField(
                "reply_to_message_id",
                ReplyToMessageId.Value.ToString()
            );

        if (AllowSendingWithoutReply is not null)
            yield return new TelegramRequestField(
                "allow_sending_without_reply",
                AllowSendingWithoutReply.Value ? "true" : "false"
            );
    }

    /// <inheritdoc />
    public override IEnumerable<TelegramRequestFile> GetRequestFiles()
    {
        yield return new TelegramRequestFile("photo", Photo);
    }
}