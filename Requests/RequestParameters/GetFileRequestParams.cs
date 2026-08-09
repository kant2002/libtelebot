namespace Telebot;

/// <summary>
/// Параметры вызова метода <c>getFile</c> — получение метаданных файла
/// (см. <see href="https://core.telegram.org/bots/api#getfile"/>) по его
/// <see cref="FileId"/>. Возвращает <see cref="Telebot.Models.File"/> с полем
/// <see cref="Telebot.Models.File.FilePath"/>, по которому потом качается сам бинарник.
/// </summary>
/// <remarks>
/// Метод сознательно разделён на два шага: <c>getFile</c> отдаёт только описание,
/// а сам файл лежит по отдельному URL <c>&lt;base&gt;/file/bot&lt;token&gt;/&lt;file_path&gt;</c>.
/// Ссылка на скачивание живёт минимум час — если протухнет, нужно повторно вызвать
/// <c>getFile</c> с тем же <see cref="FileId"/>.
/// <para/>
/// Ограничение размера — 20 MB через публичный <c>api.telegram.org</c>. Для файлов
/// крупнее используется self-hosted Bot API server (там лимит — 2 GB).
/// </remarks>
/// <param name="FileId">
/// Идентификатор файла, полученный ранее в апдейте (например,
/// <c>Message.Photo[^1].FileId</c>) или в ответе на метод отправки. Действителен только
/// в контексте этого бота — токены других ботов его не поймут.
/// </param>
public sealed record GetFileRequestParams(
    string FileId
) : TelegramRequest("getFile")
{
    /// <summary>
    /// Единственное поле <c>file_id</c> обязательно и выдаётся всегда.
    /// </summary>
    public override IEnumerable<TelegramRequestField> GetRequestFields()
    {
        yield return new TelegramRequestField("file_id", FileId);
    }
}
