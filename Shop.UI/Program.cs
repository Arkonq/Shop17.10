using Microsoft.Extensions.Configuration;
using Shop.DataAccess;
using Shop.Domain;
using System;
using System.IO;
using System.Linq;

/*
		1. Регистрация и вход (смс-код / email код) - сделать до 11.10 (Email есть на метаните)
		2. История покупок 
		3. Категории и товары (картинка в файловой системе)
		4. Покупка (корзина), оплата и доставка (PayPal/Qiwi/etc)
		5. Комментарии и рейтинги
		6. Поиск (пагинация - постраничность)

		Кто сделает 3 версии (Подключенный, автономный и EF) получит автомат на экзамене
*/

namespace Shop.UI
{
	class Program
	{
		static void Main(string[] args)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, true);

			IConfigurationRoot configurationRoot = builder.Build();

			string providerName = configurationRoot
						.GetSection("AppConfig")
						.GetChildren().Single(item => item.Key == "ProviderName")
						.Value;

			string connectionString = configurationRoot.GetConnectionString("DebugConnectionString");

			Category category = new Category
			{
				Name = "Бытовая техника",
				ImagePath = "C:/data",
			};

			Item item = new Item
			{
				Name = "Пылесос",
				ImagePath = "C:/data/items",
				Price = 25999,
				Description = "Обычный пылесос",
				CategoryId = category.Id
			};

			User user = new User
			{
				Name = "Иван",
				Surname = "Иванов",
				PhoneNumber = "123456",
				Email = "qwer@qwr.qwr",
				Address = "Twesd,12",
				Password = "password",
				VerificationCode = "1234"
			};

			using (var context = new ShopContext(connectionString))
			{
				context.Users.Add(user);
				context.Items.Add(item);
				context.Categories.Add(category);
				context.SaveChanges();
			}
		}
	}
}