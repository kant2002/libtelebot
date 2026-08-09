using System.Text.Json.Serialization;

namespace Telebot.Models;

/// <summary>
/// Файл, готовый к скачиванию с серверов Telegram
/// (см. <see href="https://core.telegram.org/bots/api#file"/>).
/// </summary>
/// <remarks>
/// Возвращается методом <c>getFile</c>. Сам бинарник Bot API отдаёт **не** в этом
/// ответе, а по отдельной ссылке <c>https://api.telegram.org/file/bot&lt;token&gt;/&lt;file_path&gt;</c>,
/// собираемой из <see cref="FilePath"/>. Ссылка гарантированно действительна не менее часа —
/// когда протухнет, нужно повторно вызвать <c>getFile</c>.
/// <para/>
/// Через публичный <c>api.telegram.org</c> лимит на скачивание — 20 MB. Через
/// self-hosted <see href="https://github.com/tdlib/telegram-bot-api">Bot API server</see>
/// — до 2 GB, и там же <see cref="FilePath"/> может прийти как <b>абсолютный локальный путь</b>
/// на диске сервера (а не относительный URL) — в этом случае скачивать по HTTP
/// не нужно, файл читается напрямую с ФС.
/// <para/>
/// Имя типа буквально повторяет Bot API. На стороне пользователя оно может
/// конфликтовать с <see cref="System.IO.File"/> — в такой ситуации выручает
/// алиас (<c>using File = Telebot.Models.File;</c>) либо квалификация полным именем.
/// </remarks>
/// <param name="FileId">
/// Идентификатор файла для использования в других методах — <c>getFile</c>,
/// <c>sendPhoto</c> (через <see cref="InputFileWithId"/>) и т. п. Действителен только
/// для этого бота и может измениться со временем.
/// </param>
/// <param name="FileUniqueId">
/// Стабильный идентификатор файла: одинаков во времени и между разными ботами.
/// <b>Нельзя</b> использовать для скачивания или пересылки — только для дедупликации
/// на стороне бота (сравнить, «этот файл я уже видел»).
/// </param>
/// <param name="FileSize">
/// Размер файла в байтах. Может отсутствовать для очень старых файлов
/// или в некоторых пограничных сценариях.
/// </param>
/// <param name="FilePath">
/// Относительный путь для скачивания. При работе с публичным API — компонент URL
/// <c>https://api.telegram.org/file/bot&lt;token&gt;/&lt;file_path&gt;</c>. При работе с
/// self-hosted Bot API server может быть абсолютным путём в локальной ФС сервера.
/// <c>null</c>, если файл по какой-то причине недоступен для скачивания.
/// </param>
public record File(
    [property: JsonPropertyName("file_id")]
    string FileId,

    [property: JsonPropertyName("file_unique_id")]
    string FileUniqueId,

    [property: JsonPropertyName("file_size")]
    long? FileSize = null,

    [property: JsonPropertyName("file_path")]
    string? FilePath = null
);