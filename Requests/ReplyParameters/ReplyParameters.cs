using System.Text.Json.Serialization;

namespace Telebot;

/// <summary>
/// Параметры ответа на сообщение
/// (<see href="https://core.telegram.org/bots/api#replyparameters"/>).
/// Заменяет устаревшую пару <c>reply_to_message_id</c> + <c>allow_sending_without_reply</c>.
/// </summary>
/// <remarks>
/// Позволяет отвечать на сообщение из другого чата (через <see cref="ChatId"/>)
/// и подсвечивать в цитате конкретный фрагмент (<see cref="Quote"/>, <see cref="QuotePosition"/>).
/// </remarks>
/// <param name="MessageId">
/// Идентификатор сообщения-цели. Обязателен, если не задан <see cref="EphemeralMessageId"/>.
/// </param>
/// <param name="ChatId">Идентификатор чата, если цель — не в текущем чате.</param>
/// <param name="EphemeralMessageId">
/// Идентификатор эфемерного сообщения в текущем чате. Альтернатива <see cref="MessageId"/>.
/// </param>
/// <param name="AllowSendingWithoutReply">Отправлять, даже если цель ответа не найдена.</param>
/// <param name="Quote">Цитируемый фрагмент, 0–1024 символа после разбора форматирования.</param>
/// <param name="QuoteParseMode">Режим разбора разметки в <see cref="Quote"/>.</param>
/// <param name="QuotePosition">Позиция цитаты в исходном сообщении (UTF-16 code units).</param>
/// <param name="ChecklistTaskId">Идентификатор задачи чек-листа, на которую отвечаем.</param>
/// <param name="PollOptionId">Идентификатор варианта опроса, на который отвечаем.</param>
public sealed record ReplyParameters(
    [property: JsonPropertyName("message_id")]
    int? MessageId = null,
    [property: JsonPropertyName("chat_id")]
    long? ChatId = null,
    [property: JsonPropertyName("ephemeral_message_id")]
    int? EphemeralMessageId = null,
    [property: JsonPropertyName("allow_sending_without_reply")]
    bool? AllowSendingWithoutReply = null,
    [property: JsonPropertyName("quote")]
    string? Quote = null,
    [property: JsonPropertyName("quote_parse_mode")]
    string? QuoteParseMode = null,
    [property: JsonPropertyName("quote_position")]
    int? QuotePosition = null,
    [property: JsonPropertyName("checklist_task_id")]
    int? ChecklistTaskId = null,
    [property: JsonPropertyName("poll_option_id")]
    string? PollOptionId = null
);