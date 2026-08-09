using System.Text.Json;

namespace Telebot;

/// <summary>
/// Параметры <c>getUpdates</c> — long-polling получение апдейтов.
/// </summary>
/// <param name="Offset">
/// Идентификатор первого ожидаемого апдейта — обычно <c>update_id</c> последнего
/// обработанного + 1. Служит подтверждением получения предыдущих.
/// </param>
/// <param name="Limit">Максимум апдейтов в ответе (1–100, по умолчанию 100).</param>
/// <param name="Timeout">Таймаут long-polling в секундах. 0 — короткий polling.</param>
/// <param name="AllowedUpdates">Список интересующих типов апдейтов.</param>
public sealed record GetUpdatesRequestParams(
    int? Offset = null,
    int? Limit = null,
    int? Timeout = null,
    IReadOnlyList<string>? AllowedUpdates = null
) : TelegramRequest("GetUpdates"), ITelegramEncodable
{
    /// <inheritdoc />
    public override IEnumerable<TelegramRequestField> GetRequestFields()
    {
        if (Offset is not null)
            yield return new TelegramRequestField("offset", Offset.Value.ToString());
        if (Limit is not null)
            yield return new TelegramRequestField("limit", Limit.Value.ToString());
        if (Timeout is not null)
            yield return new TelegramRequestField("timeout", Timeout.Value.ToString());
        if (AllowedUpdates is not null)
            yield return new TelegramRequestField("allowed_updates", JsonSerializer.Serialize(AllowedUpdates, TelebotJson.Options));
    }
}