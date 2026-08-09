using System.Text.Json.Serialization;

namespace Telebot.Models;

/// <summary>
/// Сообщение в Telegram (<see href="https://core.telegram.org/bots/api#message"/>).
/// </summary>
/// <remarks>
/// Тип сообщения определяется тем, какое из необязательных полей не равно <c>null</c>.
/// В модели покрыта только часть полей официальной спецификации.
/// </remarks>
/// <param name="MessageId">Идентификатор сообщения внутри чата.</param>
/// <param name="MessageThreadId">Идентификатор треда форума (только для супергрупп с темами).</param>
/// <param name="From">Отправитель. <c>null</c> для сообщений в каналах и авто-пересылок.</param>
/// <param name="SenderChat">Чат-отправитель (анонимный админ, канал и т. п.).</param>
/// <param name="SenderBoostCount">Число бустов, которые отправитель добавил чату.</param>
/// <param name="SenderBusinessBot">Бот, через которого сообщение отправлено от имени бизнес-аккаунта.</param>
/// <param name="Date">Дата отправки, Unix-timestamp.</param>
/// <param name="BusinessConnectionId">Идентификатор бизнес-подключения.</param>
/// <param name="Chat">Чат, в котором находится сообщение.</param>
/// <param name="IsTopicMessage"><c>true</c>, если сообщение в топике форума.</param>
/// <param name="IsAutomaticForward"><c>true</c> для автопересылок из связанного канала в дискуссионную группу.</param>
/// <param name="ReplyToMessage">Сообщение-цель ответа (без рекурсивного вложения).</param>
/// <param name="Quote">Цитируемый фрагмент исходного сообщения.</param>
/// <param name="PinnedMessage">Закреплённое сообщение (для служебных уведомлений о пине).</param>
/// <param name="Text">Текст сообщения, до 4096 символов.</param>
/// <param name="Entities">Спецсущности в тексте: упоминания, ссылки, форматирование.</param>
/// <param name="Poll">Опрос в сообщении (для сообщений типа poll).</param>
public record Message(
    [property: JsonPropertyName("message_id")]
    int MessageId,

    [property: JsonPropertyName("message_thread_id")]
    int? MessageThreadId,

    [property: JsonPropertyName("from")]
    User? From,

    [property: JsonPropertyName("sender_chat")]
    Chat? SenderChat,

    [property: JsonPropertyName("sender_boost_count")]
    int? SenderBoostCount,

    [property: JsonPropertyName("sender_business_bot")]
    User? SenderBusinessBot,

    [property: JsonPropertyName("date")]
    long Date,

    [property: JsonPropertyName("business_connection_id")]
    string? BusinessConnectionId,

    [property: JsonPropertyName("chat")]
    Chat Chat,

    [property: JsonPropertyName("is_topic_message")]
    bool? IsTopicMessage,

    [property: JsonPropertyName("is_automatic_forward")]
    bool? IsAutomaticForward,

    [property: JsonPropertyName("reply_to_message")]
    Message? ReplyToMessage,

    [property: JsonPropertyName("quote")]
    TextQuote? Quote,

    [property: JsonPropertyName("pinned_message")]
    Message? PinnedMessage,

    [property: JsonPropertyName("text")]
    string? Text,

    [property: JsonPropertyName("entities")]
    MessageEntity[]? Entities,

    [property: JsonPropertyName("poll")]
    Poll? Poll = null
);

/// <summary>
/// Спецсущность в тексте сообщения — упоминание, ссылка, форматирование
/// (<see href="https://core.telegram.org/bots/api#messageentity"/>).
/// </summary>
/// <remarks>
/// <see cref="Offset"/> и <see cref="Length"/> измеряются в <b>UTF-16 code units</b>
/// (эмодзи и символы вне BMP занимают по 2 единицы). В .NET строки уже в UTF-16,
/// поэтому <c>text.Substring(offset, length)</c> работает без пересчётов.
/// </remarks>
/// <param name="Type">
/// Тип сущности: <c>mention</c>, <c>hashtag</c>, <c>cashtag</c>, <c>bot_command</c>,
/// <c>url</c>, <c>email</c>, <c>phone_number</c>, <c>bold</c>, <c>italic</c>,
/// <c>underline</c>, <c>strikethrough</c>, <c>spoiler</c>, <c>blockquote</c>,
/// <c>expandable_blockquote</c>, <c>code</c>, <c>pre</c>, <c>text_link</c>,
/// <c>text_mention</c>, <c>custom_emoji</c>.
/// </param>
/// <param name="Offset">Смещение от начала текста в UTF-16 code units.</param>
/// <param name="Length">Длина сущности в UTF-16 code units.</param>
/// <param name="Url">URL для <c>text_link</c>.</param>
/// <param name="User">Упомянутый пользователь для <c>text_mention</c>.</param>
/// <param name="Language">Язык программирования для <c>pre</c>.</param>
/// <param name="CustomEmojiId">Идентификатор кастомного эмодзи для <c>custom_emoji</c>.</param>
public record MessageEntity(
    [property: JsonPropertyName("type")]
    string Type,

    [property: JsonPropertyName("offset")]
    int Offset,

    [property: JsonPropertyName("length")]
    int Length,

    [property: JsonPropertyName("url")]
    string? Url = null,

    [property: JsonPropertyName("user")]
    User? User = null,

    [property: JsonPropertyName("language")]
    string? Language = null,

    [property: JsonPropertyName("custom_emoji_id")]
    string? CustomEmojiId = null
);