using System.Text.Json.Serialization;

namespace Telebot.Models;

/// <summary>
/// Нажатие на кнопку инлайн-клавиатуры
/// (<see href="https://core.telegram.org/bots/api#callbackquery"/>).
/// </summary>
/// <remarks>
/// Приходит в <see cref="Update.CallbackQuery"/>. Обработчик обязан ответить
/// через <c>answerCallbackQuery</c> — иначе на кнопке продолжает крутиться индикатор.
/// </remarks>
/// <param name="Id">Идентификатор запроса для <c>answerCallbackQuery</c>.</param>
/// <param name="From">Пользователь, нажавший кнопку.</param>
/// <param name="Message">
/// Сообщение с нажатой кнопкой. <c>null</c> для сообщений из инлайн-режима или слишком старых.
/// <para/>
/// В API это union-тип <c>MaybeInaccessibleMessage</c>: обычное <see cref="Message"/>
/// или «заглушка» с одними <see cref="Message.Chat"/> и <see cref="Message.MessageId"/>.
/// Признак заглушки — <see cref="Message.Date"/> = 0.
/// </param>
/// <param name="ChatInstance">Идентификатор чата/пользователя для игр и статистики нажатий.</param>
/// <param name="Data">Данные из <c>callback_data</c> кнопки, 1–64 байта UTF-8.</param>
public record CallbackQuery(
    [property: JsonPropertyName("id")]
    string Id,

    [property: JsonPropertyName("from")]
    User From,

    [property: JsonPropertyName("message")]
    Message? Message,

    [property: JsonPropertyName("chat_instance")]
    string ChatInstance,

    [property: JsonPropertyName("data")]
    string? Data
);