namespace RedStarMath.Tests;

[TestClass]
public class LongDeccomplexTests
{
	private static readonly int MantissaLength = 300;
	private static readonly int MantissaByteLength = (int)Ceiling((MantissaLength + Log10(36)) * Log(10, 256));

	[TestMethod]
	public void ComplexTest()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
	l1:
		var m = ConstructDecimal(bytes, random);
		LongDecimal lm = new(m, MantissaLength);
		Validate();
		var actions = new[]
		{
			() =>
			{
				var op = (byte)random.Next(256);
				if ((double)m + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m += op;
				lm += op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if ((double)m - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m -= op;
				lm -= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if ((double)m * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m *= op;
				lm *= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if (op == 0)
					return;
				if ((double)m / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m /= op;
				lm /= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				var order = lm.Abs() < 1 ? -(int)(1 / lm).Order : (int)lm.Order;
				if (op.Equals(0))
					return;
				m %= op;
				lm %= op;
				ValidateRemainder(order - 52);
			}, () =>
			{
				var op = random.Next();
				if ((double)m + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m += op;
				lm += op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				if ((double)m - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m -= op;
				lm -= op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				if ((double)m * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m *= op;
				lm *= op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				if (op == 0)
					return;
				if ((double)m / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m /= op;
				lm /= op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				var order = lm.Abs() < 1 ? -(int)(1 / lm).Order : (int)lm.Order;
				if (op.Equals(0))
					return;
				m %= op;
				lm %= op;
				ValidateRemainder(order - 52);
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if ((double)m + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m += op;
				lm += op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if ((double)m - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m -= op;
				lm -= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if ((double)m * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m *= op;
				lm *= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if (op == 0)
					return;
				if ((double)m / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m /= op;
				lm /= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				var order = lm.Abs() < 1 ? -(int)(1 / lm).Order : (int)lm.Order;
				if (op.Equals(0))
					return;
				m %= op;
				lm %= op;
				ValidateRemainder(order - 52);
			}, () =>
			{
				var op = random.NextInt64();
				if ((double)m + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m += op;
				lm += (decimal)op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if ((double)m - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m -= op;
				lm -= (decimal)op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if ((double)m * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m *= op;
				lm *= (decimal)op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if (op == 0)
					return;
				if ((double)m / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m /= op;
				lm /= (decimal)op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				var order = lm.Abs() < 1 ? -(int)(1 / lm).Order : (int)lm.Order;
				if (op.Equals(0))
					return;
				m %= op;
				lm %= (decimal)op;
				ValidateRemainder(order - 52);
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if ((double)m + op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m += op;
				lm += (decimal)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if ((double)m - op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m -= op;
				lm -= (decimal)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if ((double)m * op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m *= op;
				lm *= (decimal)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if (op == 0)
					return;
				if ((double)m / op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m /= op;
				lm /= (decimal)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				var order = lm.Abs() < 1 ? -(int)(1 / lm).Order : (int)lm.Order;
				if (op.Equals(0))
					return;
				m %= op;
				lm %= (decimal)op;
				ValidateRemainder(order - 52);
			}, () =>
			{
				var op = ConstructDecimal(bytes, random);
				if ((double)m + (double)op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m += op;
				lm += op;
				Validate();
			}, () =>
			{
				var op = ConstructDecimal(bytes, random);
				if ((double)m - (double)op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m -= op;
				lm -= op;
				Validate();
			}, () =>
			{
				var op = ConstructDecimal(bytes, random);
				if ((double)m * (double)op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m *= op;
				lm *= op;
				Validate();
			}, () =>
			{
				var op = ConstructDecimal(bytes, random);
				if (op.Equals(0))
					return;
				if ((double)m / (double)op is < (double)decimal.MinValue or > (double)decimal.MaxValue)
					return;
				m /= op;
				lm /= op;
				Validate();
			}, () =>
			{
				var op = ConstructDecimal(bytes, random);
				var order = lm.Abs() < 1 ? -(int)(1 / lm).Order : (int)lm.Order;
				if (op.Equals(0))
					return;
				m %= op;
				lm %= op;
				ValidateRemainder(order - 52);
			},
		};
		for (var i = 0; i < 1000; i++)
		{
			if (random.Next(100) == 0)
				m = ConstructDecimal(bytes, random);
			lm = new(m, MantissaLength);
			actions.Random(random)();
		}
		if (counter++ < 10000)
			goto l1;
		void Validate() => Assert.AreEqual(m, (decimal)lm);
		void ValidateRemainder(int validOrder) => Assert.IsTrue(Abs(m - (decimal)lm) < ((LongDecimal)1).Shift(validOrder));
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
	public void TestAtan2()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 50000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lr = new(r, MantissaLength);
			if (random.Next(1000) != 0)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
			}
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lr2 = new(r2, MantissaLength);
			if (LongDecimal.IsNaN(lr) || LongDecimal.IsNaN(lr2) || lr == 0 && lr2 == 0)
				Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.Atan2(lr, lr2)));
			else
				Assert.IsLessThanOrEqualTo(Math.Shift(1d, -52), Abs(Atan2(r, r2) - (double)LongDecimal.Atan2(lr, lr2)));
		}
	}

	[TestMethod]
	public void TestCompareTo()
	{
		var x = new LongDecimal(123).Shift(456); // мантисса = 123, экспонента = 456
		var y = new LongDecimal(123).Shift(456);
		var result = x.CompareTo(y);
		Assert.AreEqual(0, result);
		x = new LongDecimal(100).Shift(50);
		y = new LongDecimal(200).Shift(50);
		Assert.AreEqual(-1, Sign(x.CompareTo(y)));
		Assert.AreEqual(1, Sign(y.CompareTo(x)));
		x = new LongDecimal(100).Shift(50);
		y = new LongDecimal(150).Shift(50);
		Assert.AreEqual(-1, Sign(x.CompareTo(y)));
		Assert.AreEqual(1, Sign(y.CompareTo(x)));
		x = new LongDecimal(100).Shift(1000);
		y = new LongDecimal(100).Shift(2000);
		Assert.AreEqual(-1, Sign(x.CompareTo(y)));
		Assert.AreEqual(1, Sign(y.CompareTo(x)));
		// Очень большие экспоненты
		x = new LongDecimal(1).Shift(int.MaxValue);      // экспонента = 2 147 483 647
		y = new LongDecimal(1).Shift(int.MaxValue + 1L); // экспонента = 2 147 483 648
		Assert.AreEqual(-1, Sign(x.CompareTo(y)));
		Assert.AreEqual(1, Sign(y.CompareTo(x)));
		// Очень маленькие (отрицательные) экспоненты
		x = new LongDecimal(1).Shift(int.MinValue);      // экспонента = -2 147 483 648
		y = new LongDecimal(1).Shift(int.MinValue - 1L); // экспонента = -2 147 483 649
		Assert.AreEqual(1, Sign(x.CompareTo(y)));  // x > y, т.к. -2 147 483 648 > -2 147 483 649
		Assert.AreEqual(-1, Sign(y.CompareTo(x)));
		x = new LongDecimal(-1).Shift(int.MinValue);      // экспонента = -2 147 483 648
		y = new LongDecimal(-1).Shift(int.MinValue - 1L); // экспонента = -2 147 483 649
		Assert.AreEqual(-1, Sign(x.CompareTo(y)));
		Assert.AreEqual(1, Sign(y.CompareTo(x)));
		x = new LongDecimal(500).Shift(int.MaxValue);    // очень большое число
		y = new LongDecimal(500).Shift(int.MinValue);    // очень маленькое число
		Assert.AreEqual(1, Sign(x.CompareTo(y)));
		Assert.AreEqual(-1, Sign(y.CompareTo(x)));
		x = new LongDecimal(0).Shift(0);
		y = new LongDecimal(1).Shift(0);
		Assert.AreEqual(-1, Sign(x.CompareTo(y)));
		Assert.AreEqual(1, Sign(y.CompareTo(x)));
		x = new LongDecimal(-100).Shift(50);
		y = new LongDecimal(-200).Shift(50);
		var z = new LongDecimal(100).Shift(50);
		Assert.AreEqual(1, Sign(x.CompareTo(y)));   // -100 > -200
		Assert.AreEqual(-1, Sign(y.CompareTo(x))); // -200 < -100
		Assert.AreEqual(-1, x.CompareTo(z));   // -100 < 100
		x = new LongDecimal(-100).Shift(50);
		y = new LongDecimal(-150).Shift(50);
		Assert.AreEqual(1, Sign(x.CompareTo(y)));   // -100 > -200
		Assert.AreEqual(-1, Sign(y.CompareTo(x))); // -200 < -100
		x = new LongDecimal(100).Shift(50);
		y = new LongDecimal(-100).Shift(50);
		Assert.AreEqual(1, Sign(x.CompareTo(y)));
		Assert.AreEqual(-1, Sign(y.CompareTo(x)));
		// Числа с нулевой мантиссой
		x = new LongDecimal(0).Shift(int.MaxValue);
		y = new LongDecimal(0).Shift(int.MinValue);
		Assert.AreEqual(0, Sign(x.CompareTo(y)));
		// Крайние случаи: максимально возможная разница в экспонентах
		x = new LongDecimal(1).Shift(long.MaxValue);
		y = new LongDecimal(1).Shift(long.MinValue);
		Assert.AreEqual(1, Sign(x.CompareTo(y)));
		Assert.AreEqual(-1, Sign(y.CompareTo(x)));
		x = new LongDecimal(1).Shift(1);
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000000; i++)
		{
			var m = ConstructDecimal(bytes, random);
			LongDecimal lm = new(m, MantissaLength);
			var m2 = random.Next(1000) == 0 ? m : ConstructDecimal(bytes, random);
			LongDecimal lm2 = new(m2, (int)Round(MantissaLength * (random.NextDouble() * 2 - 1)));
			if (LongDecimal.IsNaN(lm) || LongDecimal.IsNaN(lm2))
				Assert.AreEqual(int.MinValue, lm.CompareTo(lm2));
			else
				Assert.AreEqual(Sign(m.CompareTo(m2)), Sign(lm.CompareTo(lm2)));
		}
		Assert.Throws<ArgumentNullException>(() => x.CompareTo((MpzT)null!));
		Assert.Throws<ArgumentNullException>(() => x.CompareTo(null!));
	}

	[TestMethod]
	public void TestCompareToDouble()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 100000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lm = new(r, MantissaLength);
			if (random.Next(1000) != 0)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
			}
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lm2 = new(r2, MantissaLength);
			if (LongDecimal.IsNaN(lm) || LongDecimal.IsNaN(lm2))
				Assert.AreEqual(int.MinValue, lm.CompareTo(lm2));
			else
				Assert.AreEqual(Sign(r.CompareTo(r2)), Sign(lm.CompareTo(lm2)));
		}
	}

	[TestMethod]
	public void TestEquals()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(1, 501), _ => (byte)random.Next(256));
			var order = RandomOrder();
			bytes[order < 0 ? ^1 : 0] = 0;
			LongDecimal lm = new(bytes.AsSpan(), order, MantissaLength);
			if (bytes.Length - MantissaByteLength is 3 or 4 or 5)
				continue;
			ProcessA(lm);
		}
		void ProcessA(LongDecimal lm)
		{
			dynamic num = lm;
			ProcessB(lm, num);
			num = lm + 1;
			ProcessB(lm, num);
			num = lm - 1;
			ProcessB(lm, num);
			num = lm * 2;
			ProcessB(lm, num);
			num = lm / 2;
			ProcessB(lm, num);
			num = lm * 3;
			ProcessB(lm, num);
			num = lm / 3;
			ProcessB(lm, num);
			num = (byte)0;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = (short)0;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = (ushort)0;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = 0;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = 0u;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = 0L;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = 0uL;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = MpuT.Zero;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), num.Equals(lm));
			num = MpzT.Zero;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), num.Equals(lm));
			num = 0f;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = 0d;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			num = LongDecimal.Zero;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals(num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), lm.Equals((object)num));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num), num.Equals(lm));
		}
		void ProcessB(LongDecimal lm, dynamic num)
		{
			dynamic num2 = (byte)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (short)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (ushort)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (int)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (uint)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (long)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (ulong)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (MpuT)(num < 0 ? -num : num);
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), num2.Equals(lm));
			num2 = (MpzT)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), num2.Equals(lm));
			num2 = (float)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (double)num;
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals(num2));
			Assert.AreEqual(lm.Equals((MpzT)lm) && ((MpzT)lm).Equals(num2), lm.Equals((object)num2));
			num2 = (LongDecimal)num;
			Assert.AreEqual(E.SequenceEqual(lm.ToByteArray(-1), num2.ToByteArray(-1)), lm.Equals(num2));
			Assert.AreEqual(E.SequenceEqual(lm.ToByteArray(-1), num2.ToByteArray(-1)), lm.Equals((object)num2));
			Assert.AreEqual(E.SequenceEqual(lm.ToByteArray(-1), num2.ToByteArray(-1)), num2.Equals(lm));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestGeometricMean()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 100000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lm = new(r, MantissaLength);
			if (random.Next(1000) != 0)
			{
				bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
				if (random.Next(2) == 0)
					bytes.Resize(8);
				else
					bytes.ResizeLeft(8);
			}
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lm2 = new(r2, MantissaLength);
			if (LongDecimal.IsNaN(lm) || LongDecimal.IsNaN(lm2))
				Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.GeometricMean(lm, lm2)));
			else if (lm == 0 || lm2 == 0)
				Assert.IsTrue(LongDecimal.IsZero(LongDecimal.GeometricMean(lm, lm2)));
			else if (lm < 0 ^ lm2 < 0)
				Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.GeometricMean(lm, lm2)));
			else if (lm < 0)
				Assert.IsLessThanOrEqualTo(Max(Sqrt(-r) * Sqrt(-r2) / (1L << 51), double.Epsilon),
					Abs(Sqrt(-r) * Sqrt(-r2) + (double)LongDecimal.GeometricMean(lm, lm2)));
			else
				Assert.IsLessThanOrEqualTo(Max(Sqrt(r) * Sqrt(r2) / (1L << 51), double.Epsilon),
					Abs(Sqrt(r) * Sqrt(r2) - (double)LongDecimal.GeometricMean(lm, lm2)));
		}
	}

	[TestMethod]
	public void TestInverseTrigonometry()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		for (var i = 0; i < 5000; i++)
		{
			var r = random.NextDouble() * (random.Next(2) == 0 ? -1 : 1);
			var lm = new LongDecimal(r, MantissaLength);
			var lm2 = lm.Asin().Sin();
			var lm3 = lm.Acos().Cos();
			var lm4 = lm.Atan().Tan();
			Assert.IsLessThanOrEqualTo(LongDecimal.One >> MantissaLength, (lm - lm2).Abs());
			Assert.IsLessThanOrEqualTo(LongDecimal.One >> MantissaLength, (lm - lm3).Abs());
			Assert.IsLessThanOrEqualTo(LongDecimal.One >> MantissaLength - 16, (lm - lm4).Abs());
		}
	}

	[TestMethod]
	public void TestLog()
	{
		Assert.AreEqual(LongDecimal.NegativeInfinity, LongDecimal.Zero.Log());
		Assert.AreEqual(LongDecimal.PositiveInfinity, LongDecimal.PositiveInfinity.Log());
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NegativeInfinity.Log()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN.Log()));
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.One.Log());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000; i++)
		{
			var m = ConstructDecimal(bytes, random);
			LongDecimal lm = new(m, MantissaLength);
			if (LongDecimal.IsNaN(lm))
			{
				Assert.IsTrue(LongDecimal.IsNaN(lm.Log()));
				continue;
			}
			var log = lm.Log();
			var dLog = Log((double)m);
			if (LongDecimal.IsNaN(log))
				Assert.IsTrue(double.IsNaN(dLog));
			else if (LongDecimal.IsNegativeInfinity(log))
				Assert.IsTrue(double.IsNegativeInfinity(dLog));
			else if (LongDecimal.IsPositiveInfinity(log))
				Assert.IsTrue(double.IsPositiveInfinity(dLog));
			else
				Assert.IsLessThanOrEqualTo(Max(Abs(dLog), 1).Shift(-51), Abs(dLog - (double)(decimal)log));
		}
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var baseA = new MpuT(bytes.AsSpan(), RandomOrder());
			bytes.FillInPlace(random.Next(65), _ => (byte)random.Next(256));
			var shiftA = new MpzT(bytes.AsSpan(), RandomOrder());
			var a = new LongDecimal(baseA, MantissaLength).Shift(shiftA);
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var baseB = new MpuT(bytes.AsSpan(), RandomOrder());
			bytes.FillInPlace(random.Next(65), _ => (byte)random.Next(256));
			var shiftB = new MpzT(bytes.AsSpan(), RandomOrder());
			var b = new LongDecimal(baseB, MantissaLength).Shift(shiftB);
			var logA = a.Log();
			var logB = b.Log();
			var logProd = (a * b).Log();
			var logQuot = (a / b).Log();
			var logAAbs = logA.Abs();
			var logBAbs = logB.Abs();
			Assert.IsLessThanOrEqualTo(logAAbs + logBAbs >> MantissaLength - 3, (logA + logB - logProd).Abs());
			Assert.IsLessThanOrEqualTo(logAAbs + logBAbs >> MantissaLength - 3, (logA - logB - logQuot).Abs());
			var log10A = a.Log10();
			var log10B = b.Log10();
			var log10Prod = (a * b).Log10();
			var log10Quot = (a / b).Log10();
			var log10AAbs = log10A.Abs();
			var log10BAbs = log10B.Abs();
			Assert.IsLessThanOrEqualTo(log10AAbs + log10BAbs >> MantissaLength - 3, (log10A + log10B - log10Prod).Abs());
			Assert.IsLessThanOrEqualTo(log10AAbs + log10BAbs >> MantissaLength - 3, (log10A - log10B - log10Quot).Abs());
			Assert.AreEqual(a.CompareTo(b), a.Log().CompareTo(b.Log()));
			Assert.AreEqual(a.CompareTo(b), logA.CompareTo(logB));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestPower()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var longDecimalThree = new LongDecimal(3);
		Assert.AreEqual(LongDecimal.One, longDecimalThree.Power(LongDecimal.Zero));
		Assert.AreEqual(LongDecimal.PositiveInfinity, longDecimalThree.Power(LongDecimal.PositiveInfinity));
		Assert.AreEqual(LongDecimal.Zero, longDecimalThree.Power(LongDecimal.NegativeInfinity));
		Assert.IsTrue(LongDecimal.IsNaN(longDecimalThree.Power(LongDecimal.NaN)));
		Assert.AreEqual(longDecimalThree, longDecimalThree.Power(LongDecimal.One));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var uz = new MpuT(bytes.AsSpan(), RandomOrder());
			var lm = new LongDecimal(uz, MantissaLength);
			var lm2 = longDecimalThree.Power(lm).Log(longDecimalThree);
			Assert.IsLessThanOrEqualTo(lm >> MantissaLength - 2, (lm - lm2).Abs());
		}
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var @base = new MpuT(bytes.AsSpan(), RandomOrder());
			var shift = random.Next();
			var lm = new LongDecimal(@base, MantissaLength).Shift(shift);
			var lm2 = longDecimalThree.Power(lm.Log(longDecimalThree));
			Assert.IsLessThanOrEqualTo(lm >> MantissaLength - 10, (lm - lm2).Abs());
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestPowerNewton()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var longDecimalThree = new LongDecimal(3);
		Assert.AreEqual(LongDecimal.One, longDecimalThree.Power(LongDecimal.Zero));
		Assert.AreEqual(LongDecimal.PositiveInfinity, longDecimalThree.Power(LongDecimal.PositiveInfinity));
		Assert.AreEqual(LongDecimal.Zero, longDecimalThree.Power(LongDecimal.NegativeInfinity));
		Assert.IsTrue(LongDecimal.IsNaN(longDecimalThree.Power(LongDecimal.NaN)));
		Assert.AreEqual(longDecimalThree, longDecimalThree.Power(LongDecimal.One));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000; i++)
		{
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var uz = new MpuT(bytes.AsSpan(), RandomOrder());
			var lm = new LongDecimal(uz, LongDecimal.DefaultMantissaLength);
			var lm2 = longDecimalThree.Power(lm).Log(longDecimalThree);
			Assert.IsLessThanOrEqualTo(lm >> LongDecimal.DefaultMantissaLength - 4, (lm - lm2).Abs());
		}
		for (var i = 0; i < 1000; i++)
		{
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var @base = new MpuT(bytes.AsSpan(), RandomOrder());
			var shift = random.Next();
			var lm = new LongDecimal(@base, LongDecimal.DefaultMantissaLength).Shift(shift);
			var lm2 = longDecimalThree.Power(lm.Log(longDecimalThree));
			Assert.IsLessThanOrEqualTo(lm >> LongDecimal.DefaultMantissaLength - 10, (lm - lm2).Abs());
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestReciproc()
	{
		Assert.AreEqual(LongDecimal.PositiveInfinity, LongDecimal.Zero.Reciproc());
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.PositiveInfinity.Reciproc());
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.NegativeInfinity.Reciproc());
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN.Reciproc()));
		Assert.AreEqual(LongDecimal.One, LongDecimal.One.Reciproc());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var @base = new MpzT(bytes.AsSpan(), RandomOrder());
			bytes.FillInPlace(random.Next(65), _ => (byte)random.Next(256));
			var shift = new MpzT(bytes.AsSpan(), RandomOrder());
			var lm = new LongDecimal(@base, MantissaLength).Shift(shift);
			var lm2 = 1 / lm.Reciproc();
			Assert.IsLessThanOrEqualTo(lm.Abs() >> MantissaLength - 1, (lm - lm2).Abs());
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestShifts()
	{
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.Zero << 3);
		Assert.AreEqual(LongDecimal.PositiveInfinity, LongDecimal.PositiveInfinity << 3);
		Assert.AreEqual(LongDecimal.NegativeInfinity, LongDecimal.NegativeInfinity << 3);
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN << 3));
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.Zero >> 3);
		Assert.AreEqual(LongDecimal.PositiveInfinity, LongDecimal.PositiveInfinity >> 3);
		Assert.AreEqual(LongDecimal.NegativeInfinity, LongDecimal.NegativeInfinity >> 3);
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN >> 3));
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 500000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lm = new(r, MantissaLength);
			var shiftAmount = random.Next(257);
			if (double.IsNaN(r))
			{
				Assert.IsTrue(double.IsNaN((double)(lm << shiftAmount)));
				Assert.IsTrue(double.IsNaN((double)(lm << (UnsignedLongDecimal)shiftAmount)));
				Assert.IsTrue(double.IsNaN((double)(lm >> shiftAmount)));
				Assert.IsTrue(double.IsNaN((double)(lm >> (UnsignedLongDecimal)shiftAmount)));
			}
			else
			{
				Assert.AreEqual(r * Pow(10, shiftAmount), (double)(lm << shiftAmount),
					Max(Abs(r) * Pow(10, shiftAmount).Shift(-52), double.Epsilon));
				Assert.AreEqual(r * Pow(10, shiftAmount), (double)(lm << (UnsignedLongDecimal)shiftAmount),
					Max(Abs(r) * Pow(10, shiftAmount).Shift(-52), double.Epsilon));
				Assert.AreEqual(r / Pow(10, shiftAmount), (double)(lm >> shiftAmount),
					Max(Abs(r) * Pow(10, shiftAmount).Shift(-52), double.Epsilon));
				Assert.AreEqual(r / Pow(10, shiftAmount), (double)(lm >> (UnsignedLongDecimal)shiftAmount),
					Max(Abs(r) * Pow(10, shiftAmount).Shift(-52), double.Epsilon));
			}
		}
		for (var i = 0; i < 500000; i++)
		{
			bytes.FillInPlace(random.Next(259), _ => (byte)random.Next(256));
			MpuT uz = new(bytes.AsSpan(), RandomOrder());
			LongDecimal lm = new(uz, MantissaLength);
			var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
			uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
			bytes.FillInPlace(random.Next(3), _ => (byte)random.Next(256));
			bytes.PadRightInPlace(4);
			shiftAmount = BitConverter.ToInt32(bytes.AsSpan());
			Assert.IsLessThanOrEqualTo(uz.ShiftLeftDec(shiftAmount).ShiftRightRoundDec(MantissaLength),
				new LongDecimal(uz.ShiftLeftDec(shiftAmount), MantissaLength) - (lm << shiftAmount));
			Assert.IsLessThanOrEqualTo(MpuT.Max(uz >> MantissaLength, MpuT.One),
				new LongDecimal(uz.ShiftRightRoundDec(shiftAmount), MantissaLength) - (lm >> shiftAmount));
			Assert.IsLessThanOrEqualTo(uz.ShiftLeftDec(shiftAmount).ShiftRightRoundDec(MantissaLength),
				new LongDecimal(uz.ShiftLeftDec(shiftAmount), MantissaLength) - (lm << (UnsignedLongDecimal)shiftAmount));
			Assert.IsLessThanOrEqualTo(MpuT.Max(uz >> MantissaLength, MpuT.One),
				new LongDecimal(uz.ShiftRightRoundDec(shiftAmount), MantissaLength) - (lm >> (UnsignedLongDecimal)shiftAmount));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestSqrt()
	{
		Assert.AreEqual(LongDecimal.Zero, LongDecimal.Zero.Sqrt());
		Assert.AreEqual(LongDecimal.PositiveInfinity, LongDecimal.PositiveInfinity.Sqrt());
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NegativeInfinity.Sqrt()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN.Sqrt()));
		Assert.AreEqual(LongDecimal.One, LongDecimal.One.Sqrt());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 100000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lm = new(r, MantissaLength);
			if (LongDecimal.IsNaN(lm))
				Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.Sqrt(lm)));
			else
				Assert.AreEqual(Sqrt(r), (double)LongDecimal.Sqrt(lm));
		}
		for (var i = 0; i < 100000; i++)
		{
			bytes.FillInPlace(random.Next(251), _ => (byte)random.Next(256));
			var @base = new MpuT(bytes.AsSpan(), RandomOrder());
			bytes.FillInPlace(random.Next(65), _ => (byte)random.Next(256));
			var shift = new MpzT(bytes.AsSpan(), RandomOrder());
			var lm = new LongDecimal(@base, MantissaLength).Shift(shift);
			var sqrt = LongDecimal.Sqrt(lm);
			Assert.IsLessThanOrEqualTo(sqrt * sqrt >> MantissaLength - 2, (sqrt * sqrt - lm).Abs());
			Assert.IsLessThanOrEqualTo(lm >> MantissaLength - 2, (sqrt * sqrt - lm).Abs());
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestToByteArray()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 250000; i++)
		{
			bytes.FillInPlace(random.Next(1000), _ => (byte)random.Next(256));
			var order = RandomOrder();
			if (order < 0)
				bytes.Resize(Max(bytes.FindLastIndex(x => x != 0), 0) + 1);
			else
				bytes.ResizeLeft(Max(bytes.Length, 1) - Max(bytes.FindIndex(x => x != 0), 0));
			var mantissaLength = random.Next(30, Max((int)Ceiling(bytes.Length * Log10(256)), 30));
			var switcher = random.Next(1000);
			LongDecimal lm = switcher switch
			{
				0 => new(0d, mantissaLength),
				1 => new(double.PositiveInfinity, mantissaLength),
				2 => new(double.NegativeInfinity, mantissaLength),
				3 => new(double.NaN, mantissaLength),
				4 => new(double.NegativeZero, mantissaLength),
				_ => new(bytes.AsSpan(), order, mantissaLength),
			};
			LongDecimal lm2 = new(lm.ToByteArray(order, false), order, mantissaLength);
			Assert.IsTrue(LongDecimal.IsNaN(lm) && LongDecimal.IsNaN(lm2) || lm.Equals(lm2));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestToDecimal()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000000; i++)
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
			decimal m = new([i1, i2, i3, i4]);
			LongDecimal lm = new(m, MantissaLength);
			Assert.AreEqual(m, (decimal)lm);
		}
		for (var i = 0; i < 100; i++)
		{
			var lm = new LongDecimal(random.Next()).Shift(-random.Next());
			Assert.AreEqual(UnsignedLongDecimal.Zero, (UnsignedLongDecimal)lm);
		}
	}

	[TestMethod]
	public void TestToDouble()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			LongDecimal lm = new(r, MantissaLength);
			Assert.AreEqual(r, (double)lm);
		}
		for (var i = 0; i < 100; i++)
		{
			var lm = new LongDecimal(random.Next()).Shift(-random.Next());
			Assert.AreEqual(UnsignedLongDecimal.Zero, (UnsignedLongDecimal)lm);
		}
	}

	[TestMethod]
	public void TestToString()
	{
		CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
		LongDecimal longDecimal = new LongReal(1).Shift(0);
		var result = longDecimal.ToString("E6");
		Assert.AreEqual("1E+0", result);
		longDecimal = new LongReal(1).Shift(1);
		result = longDecimal.ToString("E6");
		Assert.AreEqual("2E+0", result);
		longDecimal = new LongReal(1).Shift(2);
		result = longDecimal.ToString("E6");
		Assert.AreEqual("4E+0", result);
		longDecimal = new LongReal(3).Shift(3);
		result = longDecimal.ToString("E6");
		Assert.AreEqual("2.4E+1", result);
		longDecimal = new LongReal(5).Shift(-2);
		result = longDecimal.ToString("E6");
		Assert.AreEqual("1.25E+0", result);
		longDecimal = new LongReal(123).Shift(50);
		result = longDecimal.ToString("E4");
		Assert.AreEqual("1.3849E+17", result);
		longDecimal = new LongReal(1000).Shift(-10);
		result = longDecimal.ToString("F6", CultureInfo.GetCultureInfo("en-US"));
		Assert.AreEqual("0.976563", result);
		var largeDigits = "123456789";
		var mpz = MpzT.Parse(largeDigits);
		longDecimal = new LongReal(mpz).Shift(20);
		result = longDecimal.ToString("N0", CultureInfo.GetCultureInfo("ru-RU"));
		Assert.Contains("129 453 825 982 464", result);
		longDecimal = new LongReal(1).Shift(100);
		result = longDecimal.ToString("E2");
		Assert.AreEqual("1.27E+30", result);
		foreach (var (number, format, en, ru, de) in CultureTestData())
		{
			longDecimal = number;
			var enResult = longDecimal.ToString(format, CultureInfo.GetCultureInfo("en-US"));
			Assert.AreEqual(en, enResult);
			var ruResult = longDecimal.ToString(format, CultureInfo.GetCultureInfo("ru-RU"));
			Assert.AreEqual(ru, ruResult);
			var deResult = longDecimal.ToString(format, CultureInfo.GetCultureInfo("de-DE"));
			Assert.AreEqual(de, deResult);
		}
		mpz = new MpzT(77).Power(77);
		longDecimal = new LongReal((MpzT)1).Shift(mpz);
		result = longDecimal.ToString("E6");
		Assert.AreEqual("1.358443E+5475144815987627762430594775150486533643549212522238631644821558595137232066160304681082998798877694978398467245688991276872900744519537448240061", result);
	}

	private static G.IEnumerable<(LongDecimal number, string format, string en, string ru, string de)> CultureTestData()
	{
		yield return (new LongReal(15L).Shift(12), "F2", "61,440.00", "61 440,00", "61.440,00");
		yield return (new LongReal(-987L).Shift(-8), "E3", "-3.855E+0", "-3,855E+0", "-3,855E+0");
		yield return (new(123456.789), "N5", "123,456.78900", "123 456,78900", "123.456,78900");
	}

	[TestMethod]
	public void TestTrigonometry()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		Assert.AreEqual(0, LongDecimal.Zero.Sin());
		Assert.AreEqual(1, (LongDecimal.Pi / 2).Sin());
		Assert.AreEqual(0, LongDecimal.Pi.Sin());
		Assert.AreEqual(-1, (3 * LongDecimal.Pi / 2).Sin());
		Assert.AreEqual(0, (LongDecimal.Pi * 2).Sin());
		Assert.AreEqual(-1, (-LongDecimal.Pi / 2).Sin());
		Assert.AreEqual(0, (-LongDecimal.Pi).Sin());
		Assert.AreEqual(1, (-3 * LongDecimal.Pi / 2).Sin());
		Assert.AreEqual(0, (-LongDecimal.Pi * 2).Sin());
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.PositiveInfinity.Sin()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NegativeInfinity.Sin()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN.Sin()));
		Assert.AreEqual(1, LongDecimal.Zero.Cos());
		Assert.AreEqual(0, (LongDecimal.Pi / 2).Cos());
		Assert.AreEqual(-1, LongDecimal.Pi.Cos());
		Assert.AreEqual(0, (3 * LongDecimal.Pi / 2).Cos());
		Assert.AreEqual(1, (LongDecimal.Pi * 2).Cos());
		Assert.AreEqual(0, (-LongDecimal.Pi / 2).Cos());
		Assert.AreEqual(-1, (-LongDecimal.Pi).Cos());
		Assert.AreEqual(0, (-3 * LongDecimal.Pi / 2).Cos());
		Assert.AreEqual(1, (-LongDecimal.Pi * 2).Cos());
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.PositiveInfinity.Cos()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NegativeInfinity.Cos()));
		Assert.IsTrue(LongDecimal.IsNaN(LongDecimal.NaN.Cos()));
		for (var i = 0; i < 10000; i++)
		{
			var r = Pow(2, random.NextDouble() * 128 - 64);
			LongDecimal lm = new(r, MantissaLength);
			if (LongDecimal.IsNaN(lm))
			{
				Assert.IsTrue(LongDecimal.IsNaN(lm.Sin()));
				Assert.IsTrue(LongDecimal.IsNaN(lm.Cos()));
				Assert.IsTrue(LongDecimal.IsNaN(lm.Tan()));
			}
			else
			{
				Assert.IsLessThanOrEqualTo(Math.Shift(1d, -52), Abs(Sin(r) - (double)lm.Sin()));
				Assert.IsLessThanOrEqualTo(Math.Shift(1d, -52), Abs(Cos(r) - (double)lm.Cos()));
				Assert.IsLessThanOrEqualTo(Max(Abs(Tan(r)), 1).Shift(-52), Abs(Tan(r) - (double)lm.Tan()));
			}
		}
		for (var i = 0; i < 10000; i++)
		{
			var r = (1 - random.NextDouble()) * PI / 2;
			LongDecimal lm = new(r, MantissaLength);
			Assert.IsLessThan(lm, lm.Sin());
			Assert.IsGreaterThan(lm, lm.Tan());
		}
	}

	[TestMethod]
	public void TestToUnsignedLongDecimal()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			bytes.FillInPlace(random.Next(1, 501), _ => (byte)random.Next(256));
			var order = RandomOrder();
			UnsignedLongDecimal ulm = new(bytes.AsSpan(), order, MantissaLength);
			if (bytes.Length - MantissaByteLength == 4)
				continue;
			LongDecimal lm = ulm;
			Assert.AreEqual(ulm, (UnsignedLongDecimal)lm);
		}
		for (var i = 0; i < 100; i++)
		{
			var lm = new LongDecimal(random.Next()).Shift(-random.Next());
			Assert.AreEqual(UnsignedLongDecimal.Zero, (UnsignedLongDecimal)lm);
		}
		Assert.ThrowsExactly<OverflowException>(() => (UnsignedLongDecimal)LongDecimal.PositiveInfinity);
		Assert.ThrowsExactly<OverflowException>(() => (UnsignedLongDecimal)LongDecimal.NegativeInfinity);
		Assert.ThrowsExactly<OverflowException>(() => (UnsignedLongDecimal)LongDecimal.NaN);
		int RandomOrder() => random.Next(2) * 2 - 1;
	}
}
