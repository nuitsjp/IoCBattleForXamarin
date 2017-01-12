namespace IoCBattleForXamarin.Models
{
	public interface IDatabase
	{
		ILogger Logger { get; }
		IErrorHandler ErrorHandler { get; }
	}
}
