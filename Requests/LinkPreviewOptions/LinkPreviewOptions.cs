using System.Text.Json.Serialization;

namespace Telebot;

/// <summary>
/// Настройки превью ссылок в сообщении
/// (<see href="https://core.telegram.org/bots/api#linkpreviewoptions"/>).
/// Заменяет устаревший <c>disable_web_page_preview</c>.
/// </summary>
/// <param name="IsDisabled">Полностью отключить превью ссылок.</param>
/// <param name="Url">URL для превью. По умолчанию — первая ссылка из текста.</param>
/// <param name="PreferSmallMedia">Уменьшенное превью (игнорируется, если недоступно).</param>
/// <param name="PreferLargeMedia">Увеличенное превью (игнорируется, если недоступно).</param>
/// <param name="ShowAboveText">Показать превью над текстом сообщения.</param>
public sealed record LinkPreviewOptions(
    [property: JsonPropertyName("is_disabled")]
    bool? IsDisabled = null,
    [property: JsonPropertyName("url")]
    string? Url = null,
    [property: JsonPropertyName("prefer_small_media")]
    bool? PreferSmallMedia = null,
    [property: JsonPropertyName("prefer_large_media")]
    bool? PreferLargeMedia = null,
    [property: JsonPropertyName("show_above_text")]
    bool? ShowAboveText = null
);