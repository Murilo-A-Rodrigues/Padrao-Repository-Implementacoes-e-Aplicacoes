using System.Text.Json;
using System.Text.Json.Serialization;
using Pedidos_Restaurante.Model;

public class ItemCardapioPolymorphicConverter : JsonConverter<ItemCardapio>
{
    public override ItemCardapio? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        if (root.TryGetProperty("DescricaoDetalhada", out _))
            return JsonSerializer.Deserialize<Prato>(root.GetRawText(), options);

        if (root.TryGetProperty("VolumeMl", out _))
            return JsonSerializer.Deserialize<Bebida>(root.GetRawText(), options);

        throw new JsonException("Tipo desconhecido de ItemCardapio.");
    }

    public override void Write(Utf8JsonWriter writer, ItemCardapio value, JsonSerializerOptions options)
    {
        if (value is Prato prato)
            JsonSerializer.Serialize(writer, prato, options);
        else if (value is Bebida bebida)
            JsonSerializer.Serialize(writer, bebida, options);
        else
            throw new JsonException("Tipo desconhecido de ItemCardapio.");
    }
}