USE [master]
GO
/****** Object:  Database [APIzza]    Script Date: 4/20/2023 10:40:04 AM ******/
CREATE DATABASE [APIzza]
GO
ALTER DATABASE [APIzza] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [APIzza].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [APIzza] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [APIzza] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [APIzza] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [APIzza] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [APIzza] SET ARITHABORT OFF 
GO
ALTER DATABASE [APIzza] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [APIzza] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [APIzza] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [APIzza] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [APIzza] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [APIzza] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [APIzza] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [APIzza] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [APIzza] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [APIzza] SET  ENABLE_BROKER 
GO
ALTER DATABASE [APIzza] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [APIzza] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [APIzza] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [APIzza] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [APIzza] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [APIzza] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [APIzza] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [APIzza] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [APIzza] SET  MULTI_USER 
GO
ALTER DATABASE [APIzza] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [APIzza] SET DB_CHAINING OFF 
GO
ALTER DATABASE [APIzza] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [APIzza] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [APIzza] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [APIzza] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [APIzza] SET QUERY_STORE = OFF
GO
USE [APIzza]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[homeType] [nchar](50) NOT NULL,
	[streetAddress] [nvarchar](60) NOT NULL,
	[state] [nchar](50) NOT NULL,
	[zipCode] [nchar](20) NOT NULL,
	[apartmentNumber] [nchar](10) NULL,
	[instructions] [nvarchar](250) NULL,
	[addressId] [int] IDENTITY(1,1) NOT NULL,
	[city] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[addressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Beverage]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Beverage](
	[beverageId] [int] IDENTITY(1,1) NOT NULL,
	[beveragePrice] [money] NOT NULL,
	[BeverageName] [nvarchar](50) NOT NULL,
	[availability] [bit] NOT NULL,
 CONSTRAINT [PK_Beverage] PRIMARY KEY CLUSTERED 
(
	[beverageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreateBeverage]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreateBeverage](
	[beverageId] [int] IDENTITY(1,1) NOT NULL,
	[beverageName] [nvarchar](50) NOT NULL,
	[beveragePrice] [money] NOT NULL,
	[isAvailable] [bit] NOT NULL,
	[employeeId] [int] NOT NULL,
	[description] [nvarchar](350) NOT NULL,
	[imageUrl] [nvarchar](350) NOT NULL,
 CONSTRAINT [PK_CreateBeverage] PRIMARY KEY CLUSTERED 
(
	[beverageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreatePizzaToppings]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreatePizzaToppings](
	[createPizzaId] [int] NOT NULL,
	[toppingId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreateSides]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreateSides](
	[sideId] [int] IDENTITY(1,1) NOT NULL,
	[sideName] [nvarchar](50) NOT NULL,
	[isAvailable] [bit] NOT NULL,
	[employeeId] [int] NOT NULL,
	[description] [nvarchar](350) NOT NULL,
	[sidePrice] [money] NOT NULL,
	[imageUrl] [nvarchar](350) NOT NULL,
 CONSTRAINT [PK_CreateSides] PRIMARY KEY CLUSTERED 
(
	[sideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreateSpecialtyPizzas]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreateSpecialtyPizzas](
	[createSpecialtyPizzaId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[description] [nvarchar](350) NOT NULL,
	[isAvailable] [bit] NOT NULL,
	[size] [nvarchar](50) NOT NULL,
	[sauces] [nvarchar](50) NOT NULL,
	[crusts] [nvarchar](50) NOT NULL,
	[price] [money] NOT NULL,
	[employeeId] [int] NOT NULL,
	[imageUrl] [nvarchar](350) NULL,
 CONSTRAINT [PK_CreateSpecialtyPizzas] PRIMARY KEY CLUSTERED 
(
	[createSpecialtyPizzaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CreateToppings]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreateToppings](
	[toppingId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](150) NOT NULL,
	[price] [money] NOT NULL,
 CONSTRAINT [PK_CreateToppings] PRIMARY KEY CLUSTERED 
(
	[toppingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Crust]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Crust](
	[crustId] [int] IDENTITY(1,1) NOT NULL,
	[crustName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Crust] PRIMARY KEY CLUSTERED 
(
	[crustId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password_hash] [varchar](200) NOT NULL,
	[salt] [varchar](200) NOT NULL,
	[user_role] [varchar](50) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_UserName] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_Beverage]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_Beverage](
	[orderid] [int] NOT NULL,
	[beverageid] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderPizza]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPizza](
	[orderId] [int] IDENTITY(1,1) NOT NULL,
	[orderCost] [money] NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[phoneNumber] [nvarchar](10) NOT NULL,
	[orderStatus] [nvarchar](50) NOT NULL,
	[orderDate] [date] NOT NULL,
	[employeeId] [int] NULL,
	[addressId] [int] NOT NULL,
	[ordertype] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[orderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pizza]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pizza](
	[pizzaId] [int] IDENTITY(1,1) NOT NULL,
	[sizeId] [int] NOT NULL,
	[orderId] [int] NOT NULL,
	[price] [money] NOT NULL,
	[sauceId] [int] NOT NULL,
	[crustId] [int] NOT NULL,
	[pizzaQuantity] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[description] [nvarchar](250) NULL,
 CONSTRAINT [PK_Pizza] PRIMARY KEY CLUSTERED 
(
	[pizzaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PizzaTopping]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PizzaTopping](
	[pizzaId] [int] NOT NULL,
	[toppingId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sauce]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sauce](
	[sauceId] [int] IDENTITY(1,1) NOT NULL,
	[sauceName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Sauce] PRIMARY KEY CLUSTERED 
(
	[sauceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sides]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sides](
	[sideId] [int] IDENTITY(1,1) NOT NULL,
	[sidePrice] [money] NOT NULL,
	[sidesName] [nvarchar](50) NOT NULL,
	[availability] [bit] NOT NULL,
 CONSTRAINT [PK_Sides] PRIMARY KEY CLUSTERED 
(
	[sideId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sides_Order]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sides_Order](
	[sideId] [int] NOT NULL,
	[orderId] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[sizeId] [int] IDENTITY(1,1) NOT NULL,
	[size] [nvarchar](50) NOT NULL,
	[sizePrice] [money] NOT NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[sizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topping]    Script Date: 4/20/2023 10:40:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topping](
	[toppingId] [int] IDENTITY(1,1) NOT NULL,
	[toppingPrice] [money] NOT NULL,
	[toppingName] [nvarchar](350) NOT NULL,
 CONSTRAINT [PK_Topping] PRIMARY KEY CLUSTERED 
(
	[toppingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CreateBeverage]  WITH CHECK ADD  CONSTRAINT [FK_CreateBeverage_Employee] FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([user_id])
GO
ALTER TABLE [dbo].[CreateBeverage] CHECK CONSTRAINT [FK_CreateBeverage_Employee]
GO
ALTER TABLE [dbo].[CreatePizzaToppings]  WITH CHECK ADD  CONSTRAINT [FK_CreatePizzaToppings_CreateSpecialtyPizzas1] FOREIGN KEY([createPizzaId])
REFERENCES [dbo].[CreateSpecialtyPizzas] ([createSpecialtyPizzaId])
GO
ALTER TABLE [dbo].[CreatePizzaToppings] CHECK CONSTRAINT [FK_CreatePizzaToppings_CreateSpecialtyPizzas1]
GO
ALTER TABLE [dbo].[CreatePizzaToppings]  WITH CHECK ADD  CONSTRAINT [FK_CreatePizzaToppings_CreateToppings] FOREIGN KEY([toppingId])
REFERENCES [dbo].[CreateToppings] ([toppingId])
GO
ALTER TABLE [dbo].[CreatePizzaToppings] CHECK CONSTRAINT [FK_CreatePizzaToppings_CreateToppings]
GO
ALTER TABLE [dbo].[CreateSides]  WITH CHECK ADD  CONSTRAINT [FK_CreateSides_Employee] FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([user_id])
GO
ALTER TABLE [dbo].[CreateSides] CHECK CONSTRAINT [FK_CreateSides_Employee]
GO
ALTER TABLE [dbo].[CreateSpecialtyPizzas]  WITH CHECK ADD  CONSTRAINT [FK_CreateSpecialtyPizzas_Employee] FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([user_id])
GO
ALTER TABLE [dbo].[CreateSpecialtyPizzas] CHECK CONSTRAINT [FK_CreateSpecialtyPizzas_Employee]
GO
ALTER TABLE [dbo].[Order_Beverage]  WITH CHECK ADD  CONSTRAINT [FK_Order_Beverage_Beverage] FOREIGN KEY([orderid])
REFERENCES [dbo].[Beverage] ([beverageId])
GO
ALTER TABLE [dbo].[Order_Beverage] CHECK CONSTRAINT [FK_Order_Beverage_Beverage]
GO
ALTER TABLE [dbo].[Order_Beverage]  WITH CHECK ADD  CONSTRAINT [FK_Order_Beverage_Order] FOREIGN KEY([orderid])
REFERENCES [dbo].[OrderPizza] ([orderId])
GO
ALTER TABLE [dbo].[Order_Beverage] CHECK CONSTRAINT [FK_Order_Beverage_Order]
GO
ALTER TABLE [dbo].[OrderPizza]  WITH CHECK ADD  CONSTRAINT [FK_OrderPizza_Address] FOREIGN KEY([addressId])
REFERENCES [dbo].[Address] ([addressId])
GO
ALTER TABLE [dbo].[OrderPizza] CHECK CONSTRAINT [FK_OrderPizza_Address]
GO
ALTER TABLE [dbo].[OrderPizza]  WITH CHECK ADD  CONSTRAINT [FK_OrderPizza_Employee] FOREIGN KEY([employeeId])
REFERENCES [dbo].[Employee] ([user_id])
GO
ALTER TABLE [dbo].[OrderPizza] CHECK CONSTRAINT [FK_OrderPizza_Employee]
GO
ALTER TABLE [dbo].[Pizza]  WITH CHECK ADD  CONSTRAINT [FK_Pizza_Crust] FOREIGN KEY([crustId])
REFERENCES [dbo].[Crust] ([crustId])
GO
ALTER TABLE [dbo].[Pizza] CHECK CONSTRAINT [FK_Pizza_Crust]
GO
ALTER TABLE [dbo].[Pizza]  WITH CHECK ADD  CONSTRAINT [FK_Pizza_Pizza] FOREIGN KEY([orderId])
REFERENCES [dbo].[OrderPizza] ([orderId])
GO
ALTER TABLE [dbo].[Pizza] CHECK CONSTRAINT [FK_Pizza_Pizza]
GO
ALTER TABLE [dbo].[Pizza]  WITH CHECK ADD  CONSTRAINT [FK_Pizza_Sauce] FOREIGN KEY([sauceId])
REFERENCES [dbo].[Sauce] ([sauceId])
GO
ALTER TABLE [dbo].[Pizza] CHECK CONSTRAINT [FK_Pizza_Sauce]
GO
ALTER TABLE [dbo].[Pizza]  WITH CHECK ADD  CONSTRAINT [FK_Pizza_Size] FOREIGN KEY([sizeId])
REFERENCES [dbo].[Size] ([sizeId])
GO
ALTER TABLE [dbo].[Pizza] CHECK CONSTRAINT [FK_Pizza_Size]
GO
ALTER TABLE [dbo].[PizzaTopping]  WITH CHECK ADD  CONSTRAINT [FK_PizzaTopping_Pizza] FOREIGN KEY([pizzaId])
REFERENCES [dbo].[Pizza] ([pizzaId])
GO
ALTER TABLE [dbo].[PizzaTopping] CHECK CONSTRAINT [FK_PizzaTopping_Pizza]
GO
ALTER TABLE [dbo].[PizzaTopping]  WITH CHECK ADD  CONSTRAINT [FK_PizzaTopping_Topping] FOREIGN KEY([toppingId])
REFERENCES [dbo].[Topping] ([toppingId])
GO
ALTER TABLE [dbo].[PizzaTopping] CHECK CONSTRAINT [FK_PizzaTopping_Topping]
GO
ALTER TABLE [dbo].[Sides_Order]  WITH CHECK ADD  CONSTRAINT [FK_Sides_Order_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[OrderPizza] ([orderId])
GO
ALTER TABLE [dbo].[Sides_Order] CHECK CONSTRAINT [FK_Sides_Order_Order]
GO
ALTER TABLE [dbo].[Sides_Order]  WITH CHECK ADD  CONSTRAINT [FK_Sides_Order_Sides] FOREIGN KEY([sideId])
REFERENCES [dbo].[Sides] ([sideId])
GO
ALTER TABLE [dbo].[Sides_Order] CHECK CONSTRAINT [FK_Sides_Order_Sides]
GO
USE [master]
GO
ALTER DATABASE [APIzza] SET  READ_WRITE 
GO
