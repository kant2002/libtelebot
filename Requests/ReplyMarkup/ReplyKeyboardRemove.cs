using System.Text.Json;

namespace Telebot;

/// <summary>
/// Убрать обычную (reply) клавиатуру
/// (<see href="https://core.telegram.org/bots/api#replykeyboardremove"/>).
/// </summary>
/// <param name="Selective">Убрать только у выделенных пользователей.</param>
public sealed record ReplyKeyboardRemove(
    bool? Selective = null
) : IReplyMarkup
{
    /// <inheritdoc />
    public string ToJson() => JsonSerializer.Serialize(new
    {
        remove_keyboard = true,
        selective = Selective,
    }, TelebotJson.Options);
}