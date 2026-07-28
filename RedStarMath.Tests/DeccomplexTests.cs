using McNeight;

namespace RedStarMath.Tests;

[TestClass]
public class DeccomplexTests
{
	[TestMethod]
	public void ComplexTest()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
	l1:
		var d = ConstructDecimal(bytes, random);
		var d2 = ConstructDecimal(bytes, random);
		System.Numerics.Complex nc = new((double)d, (double)d2);
		Deccomplex dc = new(d, d2);
		Validate(0);
		var actions = new[]
		{
			() =>
			{
				var op = (byte)random.Next(256);
				if ((double)dc.Real + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc += op;
				dc += op;
				Validate(op);
			}, () =>
			{
				var op = (byte)random.Next(256);
				if ((double)dc.Real - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc -= op;
				dc -= op;
				Validate(op);
			}, () =>
			{
				var op = (byte)random.Next(256);
				if ((double)dc.Real * op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc *= op;
				dc *= op;
				Validate(op);
			}, () =>
			{
				var op = (byte)random.Next(256);
				if (op == 0)
					return;
				if ((double)dc.Real / op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc /= op;
				dc /= op;
				Validate(op);
			}, () =>
			{
				var op = random.Next();
				if ((double)dc.Real + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc += op;
				dc += op;
				Validate(op);
			}, () =>
			{
				var op = random.Next();
				if ((double)dc.Real - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc -= op;
				dc -= op;
				Validate(op);
			}, () =>
			{
				var op = random.Next();
				if ((double)dc.Real * op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc *= op;
				dc *= op;
				Validate(op);
			}, () =>
			{
				var op = random.Next();
				if (op == 0)
					return;
				if ((double)dc.Real / op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc /= op;
				dc /= op;
				Validate(op);
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if ((double)dc.Real + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc += op;
				dc += op;
				Validate(op);
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if ((double)dc.Real - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc -= op;
				dc -= op;
				Validate(op);
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if ((double)dc.Real * op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc *= op;
				dc *= op;
				Validate(op);
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if (op == 0)
					return;
				if ((double)dc.Real / op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc /= op;
				dc /= op;
				Validate(op);
			}, () =>
			{
				var op = random.NextInt64();
				if ((double)dc.Real + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc += op;
				dc += op;
				Validate(op);
			}, () =>
			{
				var op = random.NextInt64();
				if ((double)dc.Real - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc -= op;
				dc -= op;
				Validate(op);
			}, () =>
			{
				var op = random.NextInt64();
				if ((double)dc.Real * op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc *= op;
				dc *= op;
				Validate(op);
			}, () =>
			{
				var op = random.NextInt64();
				if (op == 0)
					return;
				if ((double)dc.Real / op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc /= op;
				dc /= op;
				Validate(op);
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if ((double)dc.Real + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc += op;
				dc += op;
				Validate(op);
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if ((double)dc.Real - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc -= op;
				dc -= op;
				Validate(op);
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if ((double)dc.Real * op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc *= op;
				dc *= op;
				Validate(op);
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if (op == 0)
					return;
				if ((double)dc.Real / op is < (double)decimal.MinValue or > (double)decimal.MaxValue
					|| (double)dc.Imaginary / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				nc /= op;
				dc /= op;
				Validate(op);
			},
		};
		for (var i = 0; i < 1000; i++)
		{
			dc = new(d, d2);
			nc = new((double)d, (double)d2);
			actions.Random(random)();
		}
		if (counter++ < 10000)
			goto l1;
		void Validate(double op) => Assert.IsTrue(System.Numerics.Complex.Abs(nc - (System.Numerics.Complex)dc)
			<= Max(Max(Pow(10, -28), op.Shift(-50)), System.Numerics.Complex.Abs(nc).Shift(-50))
			|| nc.Real is double.NaN && (double)dc.Real is double.NaN
			|| nc.Imaginary is double.NaN && (double)dc.Imaginary is double.NaN);
	}

	private static decimal ConstructDecimal(List<byte> bytes, Random random)
	{
		bytes.FillInPlace(random.Next(17), _ => (byte)random.Next(256));
		if (random.Next(2) == 0)
			bytes.Resize(16);
		else
			bytes.ResizeLeft(16);
		var i1 = BitConverter.ToInt32(bytes.AsSpan());
		var i2 = BitConverter.ToInt32(bytes.AsSpan(4));
		var i3 = BitConverter.ToInt32(bytes.AsSpan(8));
		var i4 = BitConverter.ToInt32(bytes.AsSpan(12));
		i4 = (i4 & int.MaxValue) % 29 << 16 | i4 & int.MinValue;
		return new([i1, i2, i3, i4]);
	}

	[TestMethod]
	public void TestEquals()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			ProcessA(dc, nc);
		}
		void ProcessA(Deccomplex dc, System.Numerics.Complex nc)
		{
			dynamic num = dc;
			ProcessB(dc, nc, num);
			num = dc + 1;
			ProcessB(dc, nc, num);
			num = dc - 1;
			ProcessB(dc, nc, num);
			num = dc * 2;
			ProcessB(dc, nc, num);
			num = dc / 2;
			ProcessB(dc, nc, num);
			num = dc * 3;
			ProcessB(dc, nc, num);
			num = dc / 3;
			ProcessB(dc, nc, num);
			num = (byte)0;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = (short)0;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = (ushort)0;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = 0;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = 0u;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = 0L;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = 0uL;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = MpuT.Zero;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), num.Equals(dc));
			num = MpzT.Zero;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), num.Equals(dc));
			num = 0f;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = 0d;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num), dc.Equals((object)num));
			num = Deccomplex.Zero;
			Assert.AreEqual(dc.Equals(nc) && num.Equals(nc), dc.Equals(num));
			Assert.AreEqual(dc.Equals(nc) && num.Equals(nc), dc.Equals((object)num));
			Assert.AreEqual(dc.Equals(nc) && num.Equals(nc), num.Equals(dc));
		}
		void ProcessB(Deccomplex dc, System.Numerics.Complex nc, dynamic num)
		{
			dynamic num2 = (byte)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (short)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (ushort)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (int)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (uint)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (long)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (ulong)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (MpzT)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), num2.Equals(dc));
			num2 = (float)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (double)num.Real;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (System.Numerics.Complex)num;
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && nc.Equals(num2), dc.Equals((object)num2));
			num2 = (Deccomplex)num;
			Assert.AreEqual(dc.Equals(nc) && num2.Equals(nc), dc.Equals(num2));
			Assert.AreEqual(dc.Equals(nc) && num2.Equals(nc), dc.Equals((object)num2));
		}
	}

	[TestMethod]
	public void TestInverseTrigonometry()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		for (var i = 0; i < 5000; i++)
		{
			var d = (decimal)random.NextDouble() * (random.Next(2) == 0 ? -1 : 1);
			var d2 = (decimal)random.NextDouble() * (random.Next(2) == 0 ? -1 : 1);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			var nc2 = System.Numerics.Complex.Asin(nc);
			var dc2 = dc.Asin();
			var nc3 = System.Numerics.Complex.Acos(nc);
			var dc3 = dc.Acos();
			var nc4 = System.Numerics.Complex.Atan(nc);
			var dc4 = dc.Atan();
			Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(nc3), 1d).Shift(-50),
				System.Numerics.Complex.Abs(nc2 - (System.Numerics.Complex)dc2));
			Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(nc3), 1d).Shift(-50),
				System.Numerics.Complex.Abs(nc3 - (System.Numerics.Complex)dc3));
			Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(nc3), 1d).Shift(-50),
				System.Numerics.Complex.Abs(nc4 - (System.Numerics.Complex)dc4));
		}
	}

	[TestMethod]
	public void TestLog()
	{
		Assert.AreEqual(Deccomplex.Zero, Deccomplex.One.Log());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000; i++)
		{
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			if (Deccomplex.IsNaN(dc))
				Assert.IsTrue(Deccomplex.IsNaN(dc.Log()));
			else if (Deccomplex.IsZero(dc))
				Assert.IsTrue(nc.Equals(0));
			else
			{
				var log = System.Numerics.Complex.Log(nc);
				if (i == 29)
					;
				Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(log), 1d).Shift(-50),
					System.Numerics.Complex.Abs(log - (System.Numerics.Complex)dc.Log()));
			}
		}
	}

	[TestMethod]
	public void TestPower()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var complexThree = new Deccomplex(3, 0);
		Assert.AreEqual(Deccomplex.One, complexThree.Power(Deccomplex.Zero));
		Assert.AreEqual(complexThree, complexThree.Power(Deccomplex.One));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			if (Deccomplex.IsNaN(dc))
				Assert.IsTrue(Deccomplex.IsNaN(complexThree.Power(dc)));
			else
			{
				var log = System.Numerics.Complex.Pow(new(3, 0), nc);
				Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(log), 1d).Shift(-50),
					System.Numerics.Complex.Abs(log - (System.Numerics.Complex)complexThree.Power(dc)));
			}
		}
	}

	[TestMethod]
	public void TestReciproc()
	{
		Assert.AreEqual(Deccomplex.One, Deccomplex.One.Reciproc());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			if (dc == 0)
				Assert.ThrowsExactly<DivideByZeroException>(() => dc.Reciproc());
			else
			{
				var reciproc = System.Numerics.Complex.Reciprocal(nc);
				Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(reciproc), 1).Shift(-52),
					System.Numerics.Complex.Abs(reciproc - (System.Numerics.Complex)dc.Reciproc()));
			}
		}
	}

	[TestMethod]
	public void TestShifts()
	{
		Assert.AreEqual(Deccomplex.Zero, Deccomplex.Zero << 3);
		Assert.AreEqual(Deccomplex.Zero, Deccomplex.Zero >> 3);
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			var shiftAmount = random.Next(257);
			Assert.AreEqual(nc * Math.Shift(1d, shiftAmount), (System.Numerics.Complex)(dc << shiftAmount));
			Assert.AreEqual(nc / Math.Shift(1d, shiftAmount), (System.Numerics.Complex)(dc >> shiftAmount));
		}
	}

	[TestMethod]
	public void TestSqrt()
	{
		Assert.AreEqual(Deccomplex.Zero, Deccomplex.Zero.Sqrt());
		Assert.AreEqual(Deccomplex.One, Deccomplex.One.Sqrt());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 100000; i++)
		{
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			if (Deccomplex.IsNaN(dc))
				Assert.IsTrue(Deccomplex.IsNaN(Deccomplex.Sqrt(dc)));
			else if (System.Numerics.Complex.Abs(nc) is > (double)decimal.MaxValue)
				Assert.IsTrue(System.Numerics.Complex.Abs((System.Numerics.Complex)dc) is > (double)decimal.MaxValue);
			else
			{
				var sqrt = System.Numerics.Complex.Sqrt(nc);
				Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(sqrt), 1d).Shift(-50),
					System.Numerics.Complex.Abs(sqrt - (System.Numerics.Complex)dc.Sqrt()));
			}
		}
	}

	[TestMethod]
	public void TestToByteArray()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 250000; i++)
		{
			var order = RandomOrder();
			Deccomplex dc = random.Next(1000) switch
			{
				0 => new(0m, 0m),
				_ => new(ConstructDecimal(bytes, random), ConstructDecimal(bytes, random)),
			};
			Deccomplex dc2 = new(dc.ToByteArray(order), order);
			Assert.IsTrue(Deccomplex.IsNaN(dc) && Deccomplex.IsNaN(dc2) || dc.Equals(dc2));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestToDefaultComplex()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000000; i++)
		{
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			Assert.AreEqual(nc, (System.Numerics.Complex)dc);
		}
	}

	[TestMethod]
	public void TestToString()
	{
		//CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
		//var longReal = new Deccomplex(1).Shift(0);
		//var result = longReal.ToString("E6");
		//Assert.AreEqual("1E+0", result);
		//longReal = new Deccomplex(1).Shift(1);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("2E+0", result);
		//longReal = new Deccomplex(1).Shift(2);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("4E+0", result);
		//longReal = new Deccomplex(3).Shift(3);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("2.4E+1", result);
		//longReal = new Deccomplex(5).Shift(-2);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("1.25E+0", result);
		//longReal = new Deccomplex(123).Shift(50);
		//result = longReal.ToString("E4");
		//Assert.AreEqual("1.3849E+17", result);
		//longReal = new Deccomplex(1000).Shift(-10);
		//result = longReal.ToString("F6", CultureInfo.GetCultureInfo("en-US"));
		//Assert.AreEqual("0.976563", result);
		//var largeDigits = "123456789";
		//var mpz = MpzT.Parse(largeDigits);
		//longReal = new Deccomplex(mpz).Shift(20);
		//result = longReal.ToString("N0", CultureInfo.GetCultureInfo("ru-RU"));
		//Assert.Contains("129 453 825 982 464", result);
		//longReal = new Deccomplex(1).Shift(100);
		//result = longReal.ToString("E2");
		//Assert.AreEqual("1.27E+30", result);
		//foreach (var (number, format, en, ru, de) in CultureTestData())
		//{
		//	longReal = number;
		//	var enResult = longReal.ToString(format, CultureInfo.GetCultureInfo("en-US"));
		//	Assert.AreEqual(en, enResult);
		//	var ruResult = longReal.ToString(format, CultureInfo.GetCultureInfo("ru-RU"));
		//	Assert.AreEqual(ru, ruResult);
		//	var deResult = longReal.ToString(format, CultureInfo.GetCultureInfo("de-DE"));
		//	Assert.AreEqual(de, deResult);
		//}
		//mpz = new MpzT(77).Power(77);
		//longReal = new Deccomplex((MpzT)1).Shift(mpz);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("1.358443E+5475144815987627762430594775150486533643549212522238631644821558595137232066160304681082998798877694978398467245688991276872900744519537448240061", result);
	}

	//private static G.IEnumerable<(Deccomplex number, string format, string en, string ru, string de)> CultureTestData()
	//{
	//	yield return (new Deccomplex(15L).Shift(12), "F2", "61,440.00", "61 440,00", "61.440,00");
	//	yield return (new Deccomplex(-987L).Shift(-8), "E3", "-3.855E+0", "-3,855E+0", "-3,855E+0");
	//	yield return (new(123456.789), "N5", "123,456.78900", "123 456,78900", "123.456,78900");
	//}

	[TestMethod]
	public void TestTrigonometry()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - Deccomplex.Zero.Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (1 - (Deccomplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - Deccomplex.Pi.Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (-1 - (3 * Deccomplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - (Deccomplex.Pi << 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (-1 - (-Deccomplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - (-Deccomplex.Pi).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (1 - (-3 * Deccomplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - (-Deccomplex.Pi << 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (1 - Deccomplex.Zero.Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - (Deccomplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (-1 - Deccomplex.Pi.Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - (3 * Deccomplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (1 - (Deccomplex.Pi << 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - (-Deccomplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (-1 - (-Deccomplex.Pi).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (0 - (-3 * Deccomplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1m, -50), (1 - (-Deccomplex.Pi << 1).Cos()).Abs());
		for (var i = 0; i < 10000; i++)
		{
			var d = ConstructDecimal(bytes, random);
			var d2 = ConstructDecimal(bytes, random);
			System.Numerics.Complex nc = new((double)d, (double)d2);
			Deccomplex dc = new(d, d2);
			if (Deccomplex.IsNaN(dc))
			{
				Assert.IsTrue(Deccomplex.IsNaN(dc.Sin()));
				Assert.IsTrue(Deccomplex.IsNaN(dc.Cos()));
				Assert.IsTrue(Deccomplex.IsNaN(dc.Tan()));
			}
			else
			{
				var sin = System.Numerics.Complex.Sin(nc);
				Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(sin), 1d).Shift(-50),
					System.Numerics.Complex.Abs(sin - (System.Numerics.Complex)dc.Sin()));
				var cos = System.Numerics.Complex.Cos(nc);
				Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(cos), 1d).Shift(-50),
					System.Numerics.Complex.Abs(cos - (System.Numerics.Complex)dc.Cos()));
				Assert.IsLessThanOrEqualTo(Max(System.Numerics.Complex.Abs(sin / cos), 1d).Shift(-50),
					System.Numerics.Complex.Abs(sin / cos - (System.Numerics.Complex)dc.Tan()));
			}
		}
	}
}
