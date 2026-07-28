namespace RedStarMath.Tests;

[TestClass]
public class LongComplexTests
{
	private static readonly int MantissaLength = 1000;

	[TestMethod]
	public void ComplexTest()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
	l1:
		bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
		if (random.Next(2) == 0)
			bytes.Resize(8);
		else
			bytes.ResizeLeft(8);
		var r = BitConverter.ToDouble(bytes.AsSpan());
		bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
		if (random.Next(2) == 0)
			bytes.Resize(8);
		else
			bytes.ResizeLeft(8);
		var r2 = BitConverter.ToDouble(bytes.AsSpan());
		Complex c = new(r, r2);
		LongComplex lc = new(new LongReal(r, MantissaLength), new(r2, MantissaLength));
		Validate();
		var actions = new[]
		{
			() =>
			{
				var op = (byte)random.Next(256);
				c += op;
				lc += op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				c -= op;
				lc -= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				c *= op;
				lc *= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if (op == 0)
					return;
				c /= op;
				lc /= op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				c += op;
				lc += op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				c -= op;
				lc -= op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				c *= op;
				lc *= op;
				Validate();
			}, () =>
			{
				var op = random.Next();
				if (op == 0)
					return;
				c /= op;
				lc /= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				c += op;
				lc += op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				c -= op;
				lc -= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				c *= op;
				lc *= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if (op == 0)
					return;
				c /= op;
				lc /= op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				c += op;
				lc += (double)op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				c -= op;
				lc -= (double)op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				c *= op;
				lc *= (double)op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if (op == 0)
					return;
				c /= op;
				lc /= (double)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				c += op;
				lc += (double)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				c -= op;
				lc -= (double)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				c *= op;
				lc *= (double)op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if (op == 0)
					return;
				c /= op;
				lc /= (double)op;
				Validate();
			}, () =>
			{
				var op = BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63));
				c += op;
				lc += op;
				Validate();
			}, () =>
			{
				var op = BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63));
				c -= op;
				lc -= op;
				Validate();
			}, () =>
			{
				var op = BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63));
				c *= op;
				lc *= op;
				Validate();
			}, () =>
			{
				var op = BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63));
				if (op.Equals(0))
					return;
				c /= op;
				lc /= op;
				Validate();
			}, () =>
			{
				Complex op
					= new(BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)),
					BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)));
				c += op;
				lc += op;
				Validate();
			}, () =>
			{
				Complex op
					= new(BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)),
					BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)));
				c -= op;
				lc -= op;
				Validate();
			}, () =>
			{
				Complex op
					= new(BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)),
					BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)));
				c *= op;
				lc *= op;
				Validate();
			}, () =>
			{
				Complex op
					= new(BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)),
					BitConverter.UInt64BitsToDouble((ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63)));
				if (op.Equals(0))
					return;
				c /= op;
				lc /= op;
				Validate();
			},
		};
		for (var i = 0; i < 1000; i++)
		{
			lc = new(new LongReal(c.Real, MantissaLength), new(c.Imaginary, MantissaLength));
			actions.Random(random)();
		}
		if (counter++ < 10000)
			goto l1;
		void Validate()
		{
			if (c.Real is double.PositiveInfinity or double.NegativeInfinity or double.NaN
				|| c.Imaginary is double.PositiveInfinity or double.NegativeInfinity or double.NaN)
			{
				c = new(r, r2);
				lc = new(r, r2);
			}
			Assert.IsTrue((c - (Complex)lc).Abs() <= Max(c.Abs(), 1).Shift(-50)
			|| c.Real is double.NaN && LongReal.IsNaN(lc.Real) || c.Imaginary is double.NaN && LongReal.IsNaN(lc.Imaginary));
		}
	}

	[TestMethod]
	public void TestEquals()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			ProcessA(lc, c);
		}
		void ProcessA(LongComplex lc, Complex c)
		{
			dynamic num = lc;
			ProcessB(lc, c, num);
			num = lc + 1;
			ProcessB(lc, c, num);
			num = lc - 1;
			ProcessB(lc, c, num);
			num = lc * 2;
			ProcessB(lc, c, num);
			num = lc / 2;
			ProcessB(lc, c, num);
			num = lc * 3;
			ProcessB(lc, c, num);
			num = lc / 3;
			ProcessB(lc, c, num);
			num = (byte)0;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = (short)0;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = (ushort)0;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = 0;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = 0u;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = 0L;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = 0uL;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = MpuT.Zero;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), num.Equals(lc));
			num = MpzT.Zero;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), num.Equals(lc));
			num = 0f;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = 0d;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num), lc.Equals((object)num));
			num = LongComplex.Zero;
			Assert.AreEqual(lc.Equals(c) && num.Equals(c), lc.Equals(num));
			Assert.AreEqual(lc.Equals(c) && num.Equals(c), lc.Equals((object)num));
			Assert.AreEqual(lc.Equals(c) && num.Equals(c), num.Equals(lc));
		}
		void ProcessB(LongComplex lc, Complex c, dynamic num)
		{
			dynamic num2 = (byte)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (short)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (ushort)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (int)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (uint)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (long)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (ulong)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (MpzT)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), num2.Equals(lc));
			num2 = (float)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (double)num.Real;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (Complex)num;
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && c.Equals(num2), lc.Equals((object)num2));
			num2 = (LongComplex)num;
			Assert.AreEqual(lc.Equals(c) && num2.Equals(c), lc.Equals(num2));
			Assert.AreEqual(lc.Equals(c) && num2.Equals(c), lc.Equals((object)num2));
		}
	}

	[TestMethod]
	public void TestInverseTrigonometry()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		for (var i = 0; i < 5000; i++)
		{
			var r = random.NextDouble() * (random.Next(2) == 0 ? -1 : 1);
			var r2 = random.NextDouble() * (random.Next(2) == 0 ? -1 : 1);
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			var c2 = Complex.Asin(c);
			var lc2 = lc.Asin();
			var c3 = Complex.Acos(c);
			var lc3 = lc.Acos();
			var c4 = Complex.Atan(c);
			var lc4 = lc.Atan();
			Assert.IsLessThanOrEqualTo(Max(Complex.Abs(c3), 1d).Shift(-50),
				Complex.Abs(c2 - (Complex)lc2));
			Assert.IsLessThanOrEqualTo(Max(Complex.Abs(c3), 1d).Shift(-50),
				Complex.Abs(c3 - (Complex)lc3));
			Assert.IsLessThanOrEqualTo(Max(Complex.Abs(c3), 1d).Shift(-50),
				Complex.Abs(c4 - (Complex)lc4));
		}
	}

	[TestMethod]
	public void TestLog()
	{
		Assert.AreEqual(LongComplex.NegativeInfinity, LongComplex.Zero.Log());
		Assert.AreEqual(LongComplex.PositiveInfinity, LongComplex.PositiveInfinity.Log());
		Assert.AreEqual(new(double.PositiveInfinity, double.Pi), LongComplex.NegativeInfinity.Log());
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NaN.Log()));
		Assert.AreEqual(LongComplex.Zero, LongComplex.One.Log());
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			if (LongComplex.IsNaN(lc))
				Assert.IsTrue(LongComplex.IsNaN(lc.Log()));
			else
			{
				var log = c.Log();
				Assert.IsLessThanOrEqualTo(Max(Complex.Abs(log), 1d).Shift(-50),
					Complex.Abs(log - (Complex)lc.Log()));
			}
		}
	}

	[TestMethod]
	public void TestPower()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var complexThree = new LongComplex(3, 0);
		Assert.AreEqual(LongComplex.One, complexThree.Power(LongComplex.Zero));
		Assert.AreEqual(LongComplex.PositiveInfinity, complexThree.Power(LongComplex.PositiveInfinity));
		Assert.AreEqual(LongComplex.Zero, complexThree.Power(LongComplex.NegativeInfinity));
		Assert.IsTrue(LongComplex.IsNaN(complexThree.Power(LongComplex.NaN)));
		Assert.AreEqual(complexThree, complexThree.Power(LongComplex.One));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			if (LongComplex.IsNaN(lc))
				Assert.IsTrue(LongComplex.IsNaN(complexThree.Power(lc)));
			else
			{
				var log = Complex.Power(new(3, 0), c);
				Assert.IsLessThanOrEqualTo(Max(Complex.Abs(log), 1d).Shift(-50),
					Complex.Abs(log - (Complex)complexThree.Power(lc)));
			}
		}
	}

	[TestMethod]
	public void TestReciproc()
	{
		Assert.AreEqual(LongComplex.PositiveInfinity, LongComplex.Zero.Reciproc());
		Assert.AreEqual(LongComplex.Zero, LongComplex.PositiveInfinity.Reciproc());
		Assert.AreEqual(LongComplex.Zero, LongComplex.NegativeInfinity.Reciproc());
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NaN.Reciproc()));
		Assert.AreEqual(LongComplex.One, LongComplex.One.Reciproc());
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
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			if (lc == 0)
				Assert.IsTrue(lc.Reciproc() == LongComplex.PositiveInfinity);
			else
				Assert.IsLessThanOrEqualTo(Math.Shift(Complex.Abs(c.Reciproc()), -52),
					(c.Reciproc() - lc.Reciproc()).Abs());
		}
	}

	[TestMethod]
	public void TestShifts()
	{
		Assert.AreEqual(LongComplex.Zero, LongComplex.Zero << 3);
		Assert.AreEqual(LongComplex.PositiveInfinity, LongComplex.PositiveInfinity << 3);
		Assert.AreEqual(LongComplex.NegativeInfinity, LongComplex.NegativeInfinity << 3);
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NaN << 3));
		Assert.AreEqual(LongComplex.Zero, LongComplex.Zero >> 3);
		Assert.AreEqual(LongComplex.PositiveInfinity, LongComplex.PositiveInfinity >> 3);
		Assert.AreEqual(LongComplex.NegativeInfinity, LongComplex.NegativeInfinity >> 3);
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NaN >> 3));
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
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			var shiftAmount = random.Next(257);
			Assert.AreEqual(c * Math.Shift(1d, shiftAmount), (Complex)(lc << shiftAmount));
			Assert.AreEqual(c / Math.Shift(1d, shiftAmount), (Complex)(lc >> shiftAmount));
		}
	}

	[TestMethod]
	public void TestSqrt()
	{
		Assert.AreEqual(LongComplex.Zero, LongComplex.Zero.Sqrt());
		Assert.AreEqual(LongComplex.PositiveInfinity, LongComplex.PositiveInfinity.Sqrt());
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NegativeInfinity.Sqrt()));
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NaN.Sqrt()));
		Assert.AreEqual(LongComplex.One, LongComplex.One.Sqrt());
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
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			if (LongComplex.IsNaN(lc))
				Assert.IsTrue(LongComplex.IsNaN(LongComplex.Sqrt(lc)));
			else
			{
				var sqrt = Complex.Sqrt(c);
				Assert.IsLessThanOrEqualTo(Max(Complex.Abs(sqrt), 1d).Shift(-50),
					Complex.Abs(sqrt - (Complex)lc.Sqrt()));
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
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			var real = random.Next(1000) switch
			{
				0 => LongReal.Zero,
				1 => LongReal.PositiveInfinity,
				2 => LongReal.NegativeInfinity,
				3 => LongReal.NaN,
				4 => double.NegativeZero,
				_ => new(bytes.AsSpan(), order, LongReal.DefaultMantissaLength),
			};
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			var imaginary = random.Next(1000) switch
			{
				0 => LongReal.Zero,
				1 => LongReal.PositiveInfinity,
				2 => LongReal.NegativeInfinity,
				3 => LongReal.NaN,
				4 => double.NegativeZero,
				_ => new(bytes.AsSpan(), order, LongReal.DefaultMantissaLength),
			};
			LongComplex lc = new(real, imaginary);
			LongComplex lc2 = new(lc.ToByteArray(order), order);
			Assert.IsTrue(LongComplex.IsNaN(lc) && LongComplex.IsNaN(lc2) || lc.Equals(lc2));
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
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r = BitConverter.ToDouble(bytes.AsSpan());
			bytes.FillInPlace(random.Next(9), _ => (byte)random.Next(256));
			if (random.Next(2) == 0)
				bytes.Resize(8);
			else
				bytes.ResizeLeft(8);
			var r2 = BitConverter.ToDouble(bytes.AsSpan());
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			Assert.AreEqual(c, (Complex)lc);
		}
	}

	[TestMethod]
	public void TestToString()
	{
		//CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
		//var longReal = new LongComplex(1).Shift(0);
		//var result = longReal.ToString("E6");
		//Assert.AreEqual("1E+0", result);
		//longReal = new LongComplex(1).Shift(1);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("2E+0", result);
		//longReal = new LongComplex(1).Shift(2);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("4E+0", result);
		//longReal = new LongComplex(3).Shift(3);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("2.4E+1", result);
		//longReal = new LongComplex(5).Shift(-2);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("1.25E+0", result);
		//longReal = new LongComplex(123).Shift(50);
		//result = longReal.ToString("E4");
		//Assert.AreEqual("1.3849E+17", result);
		//longReal = new LongComplex(1000).Shift(-10);
		//result = longReal.ToString("F6", CultureInfo.GetCultureInfo("en-US"));
		//Assert.AreEqual("0.976563", result);
		//var largeDigits = "123456789";
		//var mpz = MpzT.Parse(largeDigits);
		//longReal = new LongComplex(mpz).Shift(20);
		//result = longReal.ToString("N0", CultureInfo.GetCultureInfo("ru-RU"));
		//Assert.Contains("129 453 825 982 464", result);
		//longReal = new LongComplex(1).Shift(100);
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
		//longReal = new LongComplex((MpzT)1).Shift(mpz);
		//result = longReal.ToString("E6");
		//Assert.AreEqual("1.358443E+5475144815987627762430594775150486533643549212522238631644821558595137232066160304681082998798877694978398467245688991276872900744519537448240061", result);
	}

	//private static G.IEnumerable<(LongComplex number, string format, string en, string ru, string de)> CultureTestData()
	//{
	//	yield return (new LongComplex(15L).Shift(12), "F2", "61,440.00", "61 440,00", "61.440,00");
	//	yield return (new LongComplex(-987L).Shift(-8), "E3", "-3.855E+0", "-3,855E+0", "-3,855E+0");
	//	yield return (new(123456.789), "N5", "123,456.78900", "123 456,78900", "123.456,78900");
	//}

	[TestMethod]
	public void TestTrigonometry()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - LongComplex.Zero.Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (1 - (LongComplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - LongComplex.Pi.Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (-1 - (3 * LongComplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - (LongComplex.Pi << 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (-1 - (-LongComplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - (-LongComplex.Pi).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (1 - (-3 * LongComplex.Pi >> 1).Sin()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - (-LongComplex.Pi << 1).Sin()).Abs());
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.PositiveInfinity.Sin()));
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NegativeInfinity.Sin()));
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NaN.Sin()));
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (1 - LongComplex.Zero.Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - (LongComplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (-1 - LongComplex.Pi.Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - (3 * LongComplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (1 - (LongComplex.Pi << 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - (-LongComplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (-1 - (-LongComplex.Pi).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (0 - (-3 * LongComplex.Pi >> 1).Cos()).Abs());
		Assert.IsLessThanOrEqualTo(Math.Shift(1d, -50), (1 - (-LongComplex.Pi << 1).Cos()).Abs());
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.PositiveInfinity.Cos()));
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NegativeInfinity.Cos()));
		Assert.IsTrue(LongComplex.IsNaN(LongComplex.NaN.Cos()));
		for (var i = 0; i < 10000; i++)
		{
			var r = Math.Power(2, random.NextDouble() * 128 - 64);
			var r2 = Math.Power(2, random.NextDouble() * 128 - 64);
			Complex c = new(r, r2);
			LongComplex lc = new(r, r2);
			if (LongComplex.IsNaN(lc))
			{
				Assert.IsTrue(LongComplex.IsNaN(lc.Sin()));
				Assert.IsTrue(LongComplex.IsNaN(lc.Cos()));
				Assert.IsTrue(LongComplex.IsNaN(lc.Tan()));
			}
			else
			{
				var sin = Complex.Sin(c);
				Assert.IsLessThanOrEqualTo(Max(Complex.Abs(sin), 1d).Shift(-50),
					Complex.Abs(sin - (Complex)lc.Sin()));
				var cos = Complex.Cos(c);
				Assert.IsLessThanOrEqualTo(Max(Complex.Abs(cos), 1d).Shift(-50),
					Complex.Abs(cos - (Complex)lc.Cos()));
				Assert.IsLessThanOrEqualTo(Max(Complex.Abs(sin / cos), 1d).Shift(-50),
					Complex.Abs(sin / cos - (Complex)lc.Tan()));
			}
		}
	}
}
