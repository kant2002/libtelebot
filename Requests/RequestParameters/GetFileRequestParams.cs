namespace Telebot;

/// <summary>
/// Параметры <c>getFile</c> — получение метаданных файла
/// (<see href="https://core.telegram.org/bots/api#getfile"/>).
/// </summary>
/// <remarks>
/// Возвращает <see cref="Telebot.Models.File"/> со ссылкой для скачивания.
/// Ссылка живёт минимум час; при протухании — повторный <c>getFile</c>.
/// Лимит скачивания — 20 MB через публичный API, 2 GB через self-hosted Bot API server.
/// </remarks>
/// <param name="FileId">Идентификатор файла (действителен только для этого бота).</param>
public sealed record GetFileRequestParams(
    string FileId
) : TelegramRequest("getFile")
{
    /// <inheritdoc />
    public override IEnumerable<TelegramRequestField> GetRequestFields()
    {
        yield return new TelegramRequestField("file_id", FileId);
    }
}