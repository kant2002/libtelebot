using System.Text.Json.Serialization;

namespace Telebot.Models;

/// <summary>
/// Файл на серверах Telegram (<see href="https://core.telegram.org/bots/api#file"/>).
/// </summary>
/// <remarks>
/// Возвращается методом <c>getFile</c>. Сам бинарник скачивается по отдельному URL
/// <c>&lt;base&gt;/file/bot&lt;token&gt;/&lt;file_path&gt;</c>; ссылка действует не менее часа.
/// <para/>
/// Через публичный <c>api.telegram.org</c> — лимит 20 MB на скачивание.
/// Через self-hosted <see href="https://github.com/tdlib/telegram-bot-api">Bot API server</see>
/// — до 2 GB, и <see cref="FilePath"/> может прийти абсолютным локальным путём.
/// </remarks>
/// <param name="FileId">
/// Идентификатор файла для других методов API. Действителен только для этого бота
/// и может меняться со временем.
/// </param>
/// <param name="FileUniqueId">
/// Стабильный идентификатор — одинаков во времени и между ботами.
/// Для дедупликации; для скачивания и пересылки не годится.
/// </param>
/// <param name="FileSize">Размер файла в байтах.</param>
/// <param name="FilePath">
/// Относительный путь для скачивания (или абсолютный локальный — на self-hosted API server).
/// <c>null</c>, если файл недоступен для скачивания.
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