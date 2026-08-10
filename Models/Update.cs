using System.Text.Json.Serialization;

namespace Telebot.Models;

/// <summary>
/// Входящее обновление от Telegram Bot API
/// (<see href="https://core.telegram.org/bots/api#update"/>).
/// </summary>
/// <remarks>
/// За одно обновление приходит ровно одно из необязательных полей — по нему и
/// определяется тип события. <see cref="UpdateId"/> используется для подтверждения:
/// в следующий <c>getUpdates</c> передавайте <c>offset = UpdateId + 1</c>.
/// </remarks>
/// <param name="UpdateId">Возрастающий идентификатор обновления.</param>
/// <param name="Message">Новое входящее сообщение.</param>
/// <param name="EditedMessage">Отредактированное сообщение, известное боту.</param>
/// <param name="BusinessConnection">Изменение состояния бизнес-подключения.</param>
/// <param name="BusinessMessage">Сообщение в рамках бизнес-аккаунта.</param>
/// <param name="EditedBusinessMessage">Отредактированное сообщение в бизнес-аккаунте.</param>
/// <param name="DeletedBusinessMessages">Удаление сообщений в бизнес-аккаунте.</param>
/// <param name="CallbackQuery">
/// Нажатие на инлайн-кнопку. Обработчик обязан ответить через <c>answerCallbackQuery</c>.
/// </param>
/// <param name="MyChatMember">Изменение статуса самого бота как участника чата.</param>
/// <param name="ChatMember">
/// Изменение статуса другого участника чата. Приходит только при явной подписке
/// на <c>chat_member</c> через <c>allowed_updates</c>.
/// </param>
/// <param name="PollAnswer">
/// Изменение голоса в неанонимном опросе. Для анонимных опросов не присылается.
/// </param>
public record Update(
    [property: JsonPropertyName("update_id")]
    long UpdateId,

    [property: JsonPropertyName("message")]
    Message? Message = null,

    [property: JsonPropertyName("edited_message")]
    Message? EditedMessage = null,

    [property: JsonPropertyName("business_connection")]
    BusinessConnection? BusinessConnection = null,

    [property: JsonPropertyName("business_message")]
    Message? BusinessMessage = null,

    [property: JsonPropertyName("edited_business_message")]
    Message? EditedBusinessMessage = null,

    [property: JsonPropertyName("deleted_business_messages")]
    BusinessMessagesDeleted? DeletedBusinessMessages = null,

    [property: JsonPropertyName("callback_query")]
    CallbackQuery? CallbackQuery = null,

    [property: JsonPropertyName("my_chat_member")]
    ChatMemberUpdated? MyChatMember = null,

    [property: JsonPropertyName("chat_member")]
    ChatMemberUpdated? ChatMember = null,

    [property: JsonPropertyName("poll_answer")]
    PollAnswer? PollAnswer = null
);