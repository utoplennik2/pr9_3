class Program
{
    static void Main(string[] args)
    {
        // система повідомлень
        ISender consoleSender = new ConsoleSender();
        Message textMsg = new Message(new TextMessage(), consoleSender);
        Message emailMsg = new Message(new EmailMessage(), consoleSender);

        textMsg.Send("Привіт текстово!");
        emailMsg.Send("Привіт з пошти!");

        // система управління
        IDevice tv = new TV();
        RemoteControl basicRemote = new BasicRemote(tv);

        basicRemote.PowerOn();
        basicRemote.PowerOff();

        Console.ReadLine();
    }
}
 
interface IMessageFormat  //рецепт приготування повідомлення
{
    void FormatAndSend(string message, ISender sender);
} 

class TextMessage : IMessageFormat 
{
    public void FormatAndSend(string message, ISender sender)
    {
        string formatted = "Текстове повідомлення: " + message; //додає текстове далі
        sender.Send(formatted);
    }
}

class EmailMessage : IMessageFormat
{
    public void FormatAndSend(string message, ISender sender)
    {
        string formatted = "Поштове повідомлення: " + message; //додає поштове далі
        sender.Send(formatted);
    }
}
 
interface ISender                              //кур'єр відправник
{
    void Send(string message);
}
 
class ConsoleSender : ISender                 // реалізація відправки
{
    public void Send(string message)
    {
        Console.WriteLine(message);
    }
}

class Message                                           //мост!
{
    private IMessageFormat _format;
    private ISender _sender;

    public Message(IMessageFormat format, ISender sender)
    {
        _format = format;
        _sender = sender;
    }

    public void Send(string content)
    {
        _format.FormatAndSend(content, _sender);
    }
}


interface IDevice               //інструкція для пристрою
{
    void TurnOn();
    void TurnOff();
} 
class TV : IDevice                //слідує інструкції
{
    public void TurnOn()
    {
        Console.WriteLine("TV увімкнено");
    }

    public void TurnOff()
    {
        Console.WriteLine("TV вимкнено");
    }
} 
interface RemoteControl           //пульт може
{
    void PowerOn();
    void PowerOff();
}
 
class BasicRemote : RemoteControl //управління пультом конкретного тв
{
    private IDevice _device;

    public BasicRemote(IDevice device)
    {
        _device = device;
    }

    public void PowerOn()
    {
        _device.TurnOn();
    }

    public void PowerOff()
    {
        _device.TurnOff();
    }
}