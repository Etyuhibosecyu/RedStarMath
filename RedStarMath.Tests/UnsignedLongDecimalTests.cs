namespace RedStarMath.Tests;

[TestClass]
public class UnsignedLongDecimalTests
{
	private static readonly int MantissaLength = 300;
	private static readonly int MantissaByteLength = (int)Ceiling((MantissaLength + Log10(9)) * Log(10, 256));
	private static readonly MpuT MantissaOverflow = MpuT.PowerOf10(MantissaLength);

	[TestMethod]
	public void ComplexTestMixed()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
		var writeBuffer = GC.AllocateUninitializedArray<byte>(MantissaByteLength * 3);
	l1:
		bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
		MpuT uz = new(bytes.AsSpan(), RandomOrder());
		UnsignedLongDecimal ulm = new(uz, MantissaLength);
		Validate();
		var actions = new[]
		{
			() =>
			{
				var op = (byte)random.Next(256);
				uz += op;
				ulm += op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if (op > uz)
					return;
				if (uz.DecLength <= MantissaLength + ((MpuT)op).DecLength)
					uz -= op;
				ulm -= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				uz *= op;
				ulm *= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz /= op;
				ulm = (UnsignedLongDecimal)(ulm / op);
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if (op == 0)
					return;
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(oldDecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz % op);
				if (oldDecLength > MantissaLength + ((MpuT)op).DecLength + 1)
				{
					shiftAmount = oldDecLength - MantissaLength;
					uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				}
				ulm %= op;
				Validate();
			}, () =>
			{
				var op = (byte)random.Next(256);
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz /= op;
				ulm = ulm.DivRem(op, out _);
				Validate();
			}, () =>
			{
				var op = random.Next();
				uz = (MpuT)(uz + op);
				ulm = (UnsignedLongDecimal)(ulm + op);
				Validate();
			}, () =>
			{
				var op = random.Next();
				if (op > uz)
					return;
				if (uz.DecLength <= MantissaLength + ((MpuT)op).DecLength)
					uz = (MpuT)(uz - op);
				ulm = (UnsignedLongDecimal)(ulm - op);
				Validate();
			}, () =>
			{
				var op = random.Next();
				uz = (MpuT)(uz * op);
				ulm = (UnsignedLongDecimal)(ulm * op);
				Validate();
			}, () =>
			{
				var op = random.Next();
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz / op);
				ulm = (UnsignedLongDecimal)(ulm / op);
				Validate();
			}, () =>
			{
				var op = random.Next();
				if (op == 0)
					return;
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(oldDecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz % op);
				if (oldDecLength > MantissaLength + ((MpuT)op).DecLength + 1)
				{
					shiftAmount = oldDecLength - MantissaLength;
					uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				}
				ulm = (UnsignedLongDecimal)(ulm % op);
				Validate();
			}, () =>
			{
				var op = random.Next();
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz / op);
				ulm = ulm.DivRem(op, out _);
				Validate();
			}, () =>
			{
				var op = random.Next();
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz & op);
				ulm &= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				uz += op;
				ulm += op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if (op > uz)
					return;
				if (uz.DecLength <= MantissaLength + ((MpuT)op).DecLength)
					uz -= op;
				ulm -= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				uz *= op;
				ulm *= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz /= op;
				ulm /= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if (op == 0)
					return;
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(oldDecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz % op);
				if (oldDecLength > MantissaLength + ((MpuT)op).DecLength + 1)
				{
					shiftAmount = oldDecLength - MantissaLength;
					uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				}
				ulm %= op;
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz /= op;
				ulm = ulm.DivRem(op, out _);
				Validate();
			}, () =>
			{
				var op = (uint)random.Next() + (random.Next(2) == 0 ? 0 : 1u << 31);
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz & op);
				ulm &= op;
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				uz = (MpuT)(uz + op);
				ulm = (UnsignedLongDecimal)(ulm + op);
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if (op > uz)
					return;
				if (uz.DecLength <= MantissaLength + ((MpuT)op).DecLength)
					uz = (MpuT)(uz - op);
				ulm = (UnsignedLongDecimal)(ulm - op);
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				uz = (MpuT)(uz * op);
				ulm = (UnsignedLongDecimal)(ulm * op);
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz / op);
				ulm = (UnsignedLongDecimal)(ulm / op);
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if (op == 0)
					return;
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(oldDecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz % op);
				if (oldDecLength > MantissaLength + ((MpuT)op).DecLength + 1)
				{
					shiftAmount = oldDecLength - MantissaLength;
					uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				}
				ulm = (UnsignedLongDecimal)(ulm % op);
				Validate();
			}, () =>
			{
				var op = random.NextInt64();
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(uz / op);
				ulm = ulm.DivRem(op, out _);
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				uz += op;
				ulm += op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if (Mpir.MpuCmp(op, uz) > 0)
					return;
				if (uz.DecLength <= MantissaLength + ((MpuT)op).DecLength)
					uz -= op;
				ulm -= op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				uz *= op;
				ulm *= op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz /= op;
				ulm /= op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if (op == 0)
					return;
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(oldDecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz %= op;
				if (oldDecLength > MantissaLength + ((MpuT)op).DecLength + 1)
				{
					shiftAmount = oldDecLength - MantissaLength;
					uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				}
				ulm %= op;
				Validate();
			}, () =>
			{
				var op = (ulong)random.NextInt64() + (random.Next(2) == 0 ? 0 : 1uL << 63);
				if (op == 0)
					return;
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz /= op;
				ulm = ulm.DivRem(op, out _);
				Validate();
			},
		};
		for (var i = 0; i < 1000; i++)
		{
			if (random.Next(100) == 0)
				uz = new(bytes.AsSpan(), RandomOrder());
			ulm = new(uz, MantissaLength);
			actions.Random(random)();
		}
		if (counter++ < 10000)
			goto l1;
		int RandomOrder() => random.Next(2) * 2 - 1;
		void Validate()
		{
			var decLengthDiff = uz.DecLength - MantissaLength - 1;
			var decLengthDiffClamped = Max(decLengthDiff, 0);
			using var expected = (MpzT)SafeSubtract(uz.ShiftRightRoundDec(decLengthDiffClamped), MantissaOverflow,
				decLengthDiff > 0 || uz.GetByteCount() != ulm.GetByteCount(false));
			Assert.IsTrue(ulm.TryWriteLittleEndian(writeBuffer, out var bytesWritten, false));
			using var actual = (MpzT)new MpuT(writeBuffer.AsSpan(0, Min(bytesWritten, MantissaByteLength)), -1);
			Assert.IsLessThanOrEqualTo((MpzT)MpuT.PowerOf10(decLengthDiffClamped), (expected - actual).Abs());
			if (bytesWritten > MantissaByteLength)
				Assert.AreEqual((uint)(decLengthDiffClamped + 1),
					new MpuT(writeBuffer.AsSpan(Min(bytesWritten, MantissaByteLength)..bytesWritten), -1));
		}
	}

	[TestMethod]
	public void ComplexTestMixedMantissaLength()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
		var writeBuffer = GC.AllocateUninitializedArray<byte>(MantissaByteLength * 3);
	l1:
		bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
		MpuT uz = new(bytes.AsSpan(), RandomOrder());
		var mantissaLength = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
		var maxMantissaLength = mantissaLength;
		UnsignedLongDecimal ulm = new(uz, mantissaLength);
		Validate();
		var actions = new[]
		{
			() =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				var shiftAmount = Max(uz.DecLength - mantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz += op;
				ulm += new UnsignedLongDecimal(op, mantissaLength2);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				var minMantissaLength = Min(mantissaLength, mantissaLength2);
				var shiftAmount = uz.DecLength < minMantissaLength + 1 ? 0
					: Max(uz.DecLength - minMantissaLength - 1, 0);
				var shiftAmountLite = uz.DecLength < minMantissaLength + 1 ? 0
					: Max(uz.DecLength - mantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmountLite).ShiftLeftDec(shiftAmountLite);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm = ulm >> shiftAmount << shiftAmount;
				if (random.Next(1000) == 0)
					op = uz;
				if (op > uz)
					return;
				if (uz.DecLength <= op.DecLength + maxMantissaLength)
					uz -= op;
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm -= new UnsignedLongDecimal(op, mantissaLength2);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				uz *= op;
				ulm *= new UnsignedLongDecimal(op, mantissaLength2);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var shiftAmount = Max(uz.DecLength - mantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				shiftAmount = Max(op.DecLength - mantissaLength2 - 1, 0);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				if (op == 0)
					return;
				uz /= op;
				ulm /= new UnsignedLongDecimal(op, mantissaLength2);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				UnsignedLongDecimal op = new(new MpuT(bytes.AsSpan(), RandomOrder()), mantissaLength2);
				var oldDecLength = uz.DecLength;
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				var minMantissaLength = Min(mantissaLength, mantissaLength2);
				var shiftAmount = Max(oldDecLength - minMantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				op = op >> shiftAmount << shiftAmount;
				ulm = ulm >> shiftAmount << shiftAmount;
				if (op == 0)
					return;
				uz %= (MpuT)op;
				ulm %= op;
				shiftAmount = Max(oldDecLength - minMantissaLength, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm = ulm >> shiftAmount << shiftAmount;
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				if (op == 0)
					return;
				uz /= op;
				ulm = ulm.DivRem(new UnsignedLongDecimal(op, mantissaLength2), out _);
				var shiftAmount = Max(uz.DecLength - maxMantissaLength - 1, 0);
				if (shiftAmount > 0)
					ulm = ulm >> shiftAmount << shiftAmount;
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var oldDecLength = uz.DecLength;
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				var minMantissaLength = Min(mantissaLength, mantissaLength2);
				var shiftAmount = Max(Max(oldDecLength, op.DecLength) - minMantissaLength - 1, 0);
				var shiftAmountLite = Max(Max(oldDecLength, op.DecLength) - mantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmountLite).ShiftLeftDec(shiftAmountLite);
				ulm = ulm >> shiftAmountLite << shiftAmountLite;
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				if (op.DecLength > uz.DecLength + maxMantissaLength || uz.DecLength > op.DecLength + maxMantissaLength)
					uz = 0;
				else
					uz &= op;
				ulm &= new UnsignedLongDecimal(op, mantissaLength2);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var oldDecLength = uz.DecLength;
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				var minMantissaLength = Min(mantissaLength, mantissaLength2);
				var shiftAmount = Max(Max(oldDecLength, op.DecLength) - minMantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				if (uz.DecLength < op.DecLength && op.DecLength > uz.DecLength + maxMantissaLength)
					uz = op;
				else if (uz.DecLength <= op.DecLength + maxMantissaLength)
					uz |= op;
				ulm = ulm >> shiftAmount << shiftAmount;
				ulm |= new UnsignedLongDecimal(op, mantissaLength2);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var oldDecLength = uz.DecLength;
				maxMantissaLength = Max(mantissaLength, mantissaLength2);
				var minMantissaLength = Min(mantissaLength, mantissaLength2);
				var shiftAmount = Max(Max(oldDecLength, op.DecLength) - minMantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				if (uz.DecLength < op.DecLength && op.DecLength > uz.DecLength + maxMantissaLength)
					uz = op;
				else if (uz.DecLength <= op.DecLength + maxMantissaLength)
					uz ^= op;
				ulm = ulm >> shiftAmount << shiftAmount;
				ulm ^= new UnsignedLongDecimal(op, mantissaLength2);
				Validate();
			},
		};
		for (var i = 0; i < 1000; i++)
		{
			mantissaLength = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
			ulm = new(uz, mantissaLength);
			actions.Random(random)();
		}
		if (counter++ < 10000)
			goto l1;
		int RandomOrder() => random.Next(2) * 2 - 1;
		void Validate()
		{
			var decLengthDiff = uz.DecLength - maxMantissaLength - 1;
			var decLengthDiffClamped = Max(decLengthDiff, 0);
			using var expected = (MpzT)SafeSubtract(uz.ShiftRightRoundDec(decLengthDiffClamped),
				MpuT.PowerOf10(maxMantissaLength), decLengthDiff > 0 || uz.GetByteCount() != ulm.GetByteCount(false));
			Assert.IsTrue(ulm.TryWriteLittleEndian(writeBuffer, out var bytesWritten, false));
			var maxMantissaByteLength = Min(bytesWritten, (int)Ceiling((maxMantissaLength + Log10(9)) * Log(10, 256)));
			using var actual = (MpzT)new MpuT(writeBuffer.AsSpan(0, maxMantissaByteLength), -1);
			Assert.IsLessThanOrEqualTo((MpzT)MpuT.PowerOf10(maxMantissaLength), (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(expected >> 2, (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(actual >> 2, (expected - actual).Abs());
			if (bytesWritten > maxMantissaByteLength)
				Assert.AreEqual((uint)(decLengthDiffClamped + 1),
					new MpuT(writeBuffer.AsSpan(Min(bytesWritten, maxMantissaByteLength)..bytesWritten), -1));
		}
	}

	[TestMethod]
	public void ComplexTestSame()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
		var writeBuffer = GC.AllocateUninitializedArray<byte>(MantissaByteLength * 3);
	l1:
		bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
		MpuT uz = new(bytes.AsSpan(), RandomOrder());
		UnsignedLongDecimal ulm = new(uz, MantissaLength);
		Validate();
		var actions = new[]
		{
			() =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				uz += op;
				ulm += new UnsignedLongDecimal(op, MantissaLength);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				shiftAmount = Max(op.DecLength - MantissaLength - 1, 0);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				if (random.Next(1000) == 0)
					op = uz;
				if (op > uz)
					return;
				if (uz.DecLength <= op.DecLength + MantissaLength)
					uz -= op;
				ulm -= new UnsignedLongDecimal(op, MantissaLength);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				uz *= op;
				ulm *= new UnsignedLongDecimal(op, MantissaLength);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				shiftAmount = Max(op.DecLength - MantissaLength - 1, 0);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				if (op == 0)
					return;
				uz /= op;
				ulm /= new UnsignedLongDecimal(op, MantissaLength);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				if (op == 0)
					return;
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(oldDecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				shiftAmount = Max(op.DecLength - MantissaLength - 1, 0);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz %= op;
				ulm %= new UnsignedLongDecimal(op, MantissaLength);
				shiftAmount = Max(oldDecLength - MantissaLength, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm = ulm >> shiftAmount << shiftAmount;
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				shiftAmount = Max(op.DecLength - MantissaLength - 1, 0);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				if (op == 0)
					return;
				uz /= op;
				ulm = ulm.DivRem(new UnsignedLongDecimal(op, MantissaLength), out _);
				var decLengthDiffClamped = Max(uz.DecLength - MantissaLength - 1, 0);
				if (decLengthDiffClamped > 0)
					ulm = ulm >> decLengthDiffClamped << decLengthDiffClamped;
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				UnsignedLongDecimal op = new(new MpuT(bytes.AsSpan(), RandomOrder()), MantissaLength);
				if (op == 0)
					return;
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(oldDecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz %= (MpuT)op;
				shiftAmount = Max(oldDecLength - MantissaLength, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm %= op;
				ulm = ulm >> shiftAmount << shiftAmount;
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(Max(oldDecLength, op.DecLength) - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm = ulm >> shiftAmount << shiftAmount;
				if (uz.DecLength < op.DecLength && op.DecLength > uz.DecLength + MantissaLength
					|| uz.DecLength > op.DecLength + MantissaLength)
					uz = 0;
				else
					uz &= op;
				ulm &= new UnsignedLongDecimal(op, MantissaLength);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(Max(oldDecLength, op.DecLength) - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm = ulm >> shiftAmount << shiftAmount;
				if (uz.DecLength < op.DecLength && op.DecLength > uz.DecLength + MantissaLength)
					uz = op;
				else if (uz.DecLength <= op.DecLength + MantissaLength)
					uz |= op;
				ulm |= new UnsignedLongDecimal(op, MantissaLength);
				Validate();
			}, () =>
			{
				bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
				MpuT op = new(bytes.AsSpan(), RandomOrder());
				var oldDecLength = uz.DecLength;
				var shiftAmount = Max(Max(oldDecLength, op.DecLength) - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				op = op.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				ulm = ulm >> shiftAmount << shiftAmount;
				if (uz.DecLength < op.DecLength && op.DecLength > uz.DecLength + MantissaLength)
					uz = op;
				else if (uz.DecLength <= op.DecLength + MantissaLength)
					uz ^= op;
				ulm ^= new UnsignedLongDecimal(op, MantissaLength);
				Validate();
			}, () =>
			{
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (uint)(int)uz;
				ulm = (uint)(int)ulm;
				Validate();
			}, () =>
			{
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (uint)uz;
				ulm = (uint)ulm;
				Validate();
			}, () =>
			{
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (ulong)(long)uz;
				ulm = (ulong)(long)ulm;
				Validate();
			}, () =>
			{
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (ulong)uz;
				ulm = (ulong)ulm;
				Validate();
			}, () =>
			{
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(double)uz;
				ulm = new((double)ulm, MantissaLength);
				Validate();
			}, () =>
			{
				var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
				uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
				uz = (MpuT)(decimal)uz;
				ulm = new((decimal)ulm, MantissaLength);
				Validate();
			},
		};
		for (var i = 0; i < 1000; i++)
		{
			ulm = new(uz, MantissaLength);
			actions.Random(random)();
		}
		if (counter++ < 10000)
			goto l1;
		int RandomOrder() => random.Next(2) * 2 - 1;
		void Validate()
		{
			var decLengthDiff = uz.DecLength - MantissaLength - 1;
			var decLengthDiffClamped = Max(decLengthDiff, 0);
			using var expected = (MpzT)SafeSubtract(uz.ShiftRightRoundDec(decLengthDiffClamped), MantissaOverflow,
				decLengthDiff > 0 || uz.GetByteCount() != ulm.GetByteCount(false));
			Assert.IsTrue(ulm.TryWriteLittleEndian(writeBuffer, out var bytesWritten, false));
			using var actual = (MpzT)new MpuT(writeBuffer.AsSpan(0, Min(bytesWritten, MantissaByteLength)), -1);
			Assert.IsLessThanOrEqualTo((MpzT)MpuT.PowerOf10(decLengthDiffClamped), (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(expected >> 2, (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(actual >> 2, (expected - actual).Abs());
			if (bytesWritten > MantissaByteLength)
				Assert.AreEqual((uint)(decLengthDiffClamped + 1),
					new MpuT(writeBuffer.AsSpan(Min(bytesWritten, MantissaByteLength)..bytesWritten), -1));
		}
	}

	[TestMethod]
	public void ConversionTest()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		var writeBuffer = GC.AllocateUninitializedArray<byte>(MantissaByteLength * 3);
		var counter = 0;
	l1:
		for (var i = 0; i < 1000; i++)
		{
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			MpuT uz = new(bytes.AsSpan(), RandomOrder());
			using UnsignedLongDecimal ulm = new(uz, MantissaLength);
			var decLengthDiff = uz.DecLength - MantissaLength - 1;
			var decLengthDiffClamped = Max(decLengthDiff, 0);
			if (decLengthDiffClamped > 0)
				uz = uz.ShiftRightRoundDec(decLengthDiffClamped).ShiftLeftDec(decLengthDiffClamped);
			using var expected = (MpzT)SafeSubtract(uz.ShiftRightRoundDec(decLengthDiffClamped), MantissaOverflow,
				decLengthDiff > 0 || uz.GetByteCount() != ulm.GetByteCount(false));
			Assert.IsTrue(ulm.TryWriteLittleEndian(writeBuffer, out var bytesWritten, false));
			using var actual = (MpzT)new MpuT(writeBuffer.AsSpan(0, Min(bytesWritten, MantissaByteLength)), -1);
			Assert.AreEqual(expected, actual);
			if (bytesWritten > MantissaByteLength)
				Assert.AreEqual((uint)(decLengthDiffClamped + 1),
					new MpuT(writeBuffer.AsSpan(Min(bytesWritten, MantissaByteLength)..bytesWritten), -1));
		}
		if (counter++ < 2500)
			goto l1;
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestAdd()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
		var writeBuffer = GC.AllocateUninitializedArray<byte>(MantissaByteLength * 3);
	l1:
		bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
		MpuT uz = new(bytes.AsSpan(), RandomOrder());
		var mantissaLength = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
		var maxMantissaLength = mantissaLength;
		UnsignedLongDecimal ulm = new(uz, mantissaLength);
		Validate();
		void Action()
		{
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
			MpuT op = new(bytes.AsSpan(), RandomOrder());
			maxMantissaLength = Max(mantissaLength, mantissaLength2);
			uz += op;
			ulm += new UnsignedLongDecimal(op, mantissaLength2);
			Validate();
		}
		for (var i = 0; i < 1000; i++)
		{
			mantissaLength = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
			ulm = new(uz, mantissaLength);
			Action();
		}
		if (counter++ < 10000)
			goto l1;
		int RandomOrder() => random.Next(2) * 2 - 1;
		void Validate()
		{
			var decLengthDiff = uz.DecLength - maxMantissaLength - 1;
			var decLengthDiffClamped = Max(decLengthDiff, 0);
			using var expected = (MpzT)SafeSubtract(uz.ShiftRightRoundDec(decLengthDiffClamped),
				MpuT.PowerOf10(maxMantissaLength), decLengthDiff > 0 || uz.GetByteCount() != ulm.GetByteCount(false));
			Assert.IsTrue(ulm.TryWriteLittleEndian(writeBuffer, out var bytesWritten, false));
			var maxMantissaByteLength = Min(bytesWritten, (int)Ceiling((maxMantissaLength + Log10(9)) * Log(10, 256)));
			using var actual = (MpzT)new MpuT(writeBuffer.AsSpan(0, maxMantissaByteLength), -1);
			Assert.IsLessThanOrEqualTo((MpzT)MpuT.PowerOf10(maxMantissaLength), (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(expected >> 2, (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(actual >> 2, (expected - actual).Abs());
			if (bytesWritten > maxMantissaByteLength)
				Assert.AreEqual((uint)(decLengthDiffClamped + 1),
					new MpuT(writeBuffer.AsSpan(Min(bytesWritten, maxMantissaByteLength)..bytesWritten), -1));
		}
	}

	[TestMethod]
	public void TestCompareTo()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(1000), _ => (byte)random.Next(256));
			using UnsignedLongDecimal ulm = new(bytes.AsSpan(), RandomOrder(), MantissaLength);
			if (bytes.Length - MantissaByteLength is 3 or 4)
				continue;
			ProcessA(ulm);
		}
		void ProcessA(UnsignedLongDecimal ulm)
		{
			dynamic num = ulm;
			ProcessB(ulm, num);
			num = ulm + 1;
			ProcessB(ulm, num);
			if (ulm.CompareTo(0) != 0)
			{
				num = ulm - 1;
				ProcessB(ulm, num);
			}
			num = ulm * 2;
			ProcessB(ulm, num);
			num = ulm / 2;
			ProcessB(ulm, num);
			num = ulm * 3;
			ProcessB(ulm, num);
			num = ulm / 3;
			ProcessB(ulm, num);
			bytes.FillInPlace(random.Next(1000), _ => (byte)random.Next(256));
			num = new UnsignedLongDecimal(bytes.AsSpan(), RandomOrder(), (int)Round(Pow(2, random.NextDouble() * 2) * 150));
			var (ulm2, num2) = (ulm, num);
			while (ulm2.DecLength > int.MaxValue || num2.DecLength > int.MaxValue)
				(ulm2, num2) = (ulm2.DecLength, num2.DecLength);
			Assert.AreEqual(Sign(((MpuT)ulm2).CompareTo((MpuT)num2)), ulm.CompareTo(num));
			num = (byte)0;
			Validate(ulm, num);
			num = (short)0;
			Validate(ulm, num);
			num = (ushort)0;
			Validate(ulm, num);
			num = 0;
			Validate(ulm, num);
			num = 0u;
			Validate(ulm, num);
			num = 0L;
			Validate(ulm, num);
			num = 0uL;
			Validate(ulm, num);
			num = MpuT.Zero;
			Validate2(ulm, num);
			num = MpzT.Zero;
			Validate2(ulm, num);
			num = UnsignedLongDecimal.Zero;
			Validate2(ulm, num);
		}
		void ProcessB(UnsignedLongDecimal ulm, dynamic num)
		{
			dynamic num2 = (byte)num;
			Validate(ulm, num2);
			num2 = (short)num is var si && si < 0 ? ~si : si;
			Validate(ulm, num2);
			num2 = (ushort)num;
			Validate(ulm, num2);
			num2 = (int)num is var i && i < 0 ? ~i : i;
			Validate(ulm, num2);
			num2 = (uint)num;
			Validate(ulm, num2);
			num2 = (long)num is var li && li < 0 ? ~li : li;
			Validate(ulm, num2);
			num2 = (ulong)num;
			Validate(ulm, num2);
			num2 = (MpuT)num;
			Validate2(ulm, num2);
			num2 = (MpzT)num;
			Validate2(ulm, num2);
			num2 = new UnsignedLongDecimal(num, MantissaLength);
			int comp;
			if (num2.ToByteArray(1) is not byte[] rightArr)
				comp = 0;
			else if (ulm.ToByteArray(1) is var leftArr
				&& leftArr.Length.CompareTo(rightArr.Length) is var lenDiff && lenDiff != 0)
				comp = Sign(lenDiff);
			else if (MemoryExtensions.CommonPrefixLength(leftArr, rightArr) is var len
				&& len == leftArr.Length && len == rightArr.Length)
				comp = 0;
			else if (len == leftArr.Length)
				comp = -1;
			else if (len == rightArr.Length)
				comp = 1;
			else
				comp = Sign(leftArr[len].CompareTo(rightArr[len]));
			Assert.AreEqual(comp, Sign(ulm.CompareTo(num2)));
			Assert.AreEqual(comp, Sign(ulm.CompareTo((object)num2)));
			Assert.AreEqual(comp, -Sign(num2.CompareTo(ulm)));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
		static void Validate(UnsignedLongDecimal ulm, dynamic num2)
		{
			Assert.AreEqual(ulm.Equals((MpuT)ulm) ? Sign(((MpuT)ulm).CompareTo(num2)) : 1, Sign(ulm.CompareTo(num2)));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) ? Sign(((MpuT)ulm).CompareTo(num2)) : 1, Sign(ulm.CompareTo((object)num2)));
		}
		static void Validate2(UnsignedLongDecimal ulm, dynamic num)
		{
			Assert.AreEqual(ulm.Equals((MpuT)ulm) ? Sign(((MpuT)ulm).CompareTo(num)) : 1, Sign(ulm.CompareTo(num)));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) ? Sign(((MpuT)ulm).CompareTo(num)) : 1, Sign(ulm.CompareTo((object)num)));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) ? Sign(((MpuT)ulm).CompareTo(num)) : 1, -Sign(num.CompareTo(ulm)));
		}
	}

	[TestMethod]
	public void TestEquals()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(1000), _ => (byte)random.Next(256));
			using UnsignedLongDecimal ulm = new(bytes.AsSpan(), RandomOrder(), MantissaLength);
			if (bytes.Length - MantissaByteLength is 3 or 4)
				continue;
			ProcessA(ulm);
		}
		void ProcessA(UnsignedLongDecimal ulm)
		{
			dynamic num = ulm;
			ProcessB(ulm, num);
			num = ulm + 1;
			ProcessB(ulm, num);
			if (ulm.CompareTo(0) != 0)
			{
				num = ulm - 1;
				ProcessB(ulm, num);
			}
			num = ulm * 2;
			ProcessB(ulm, num);
			num = ulm / 2;
			ProcessB(ulm, num);
			num = ulm * 3;
			ProcessB(ulm, num);
			num = ulm / 3;
			ProcessB(ulm, num);
			num = (byte)0;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			num = (short)0;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			num = (ushort)0;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			num = 0;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			num = 0u;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			num = 0L;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			num = 0uL;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			num = MpuT.Zero;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), num.Equals(ulm));
			num = MpzT.Zero;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), num.Equals(ulm));
			num = UnsignedLongDecimal.Zero;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals(num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), ulm.Equals((object)num));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num), num.Equals(ulm));
		}
		void ProcessB(UnsignedLongDecimal ulm, dynamic num)
		{
			dynamic num2 = (byte)num;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			num2 = (short)num is var si && si < 0 ? ~si : si;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			num2 = (ushort)num;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			num2 = (int)num is var i && i < 0 ? ~i : i;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			num2 = (uint)num;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			num2 = (long)num is var li && li < 0 ? ~li : li;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			num2 = (ulong)num;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			num2 = (MpuT)num;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), num2.Equals(ulm));
			num2 = (MpzT)num;
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals(num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), ulm.Equals((object)num2));
			Assert.AreEqual(ulm.Equals((MpuT)ulm) && ((MpuT)ulm).Equals(num2), num2.Equals(ulm));
			num2 = new UnsignedLongDecimal(num, MantissaLength);
			Assert.AreEqual(E.SequenceEqual(ulm.ToByteArray(-1), num2.ToByteArray(-1)), ulm.Equals(num2));
			Assert.AreEqual(E.SequenceEqual(ulm.ToByteArray(-1), num2.ToByteArray(-1)), ulm.Equals((object)num2));
			Assert.AreEqual(E.SequenceEqual(ulm.ToByteArray(-1), num2.ToByteArray(-1)), num2.Equals(ulm));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestIncrementDecrement()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			bytes.FillInPlace(random.Next(259), _ => (byte)random.Next(256));
			MpuT uz = new(bytes.AsSpan(), RandomOrder());
			UnsignedLongDecimal ulm = new(uz, MantissaLength);
			var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
			uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
			Assert.AreEqual(++uz, ++ulm);
			Assert.AreEqual(uz++, ulm++);
			Assert.AreEqual(--uz, --ulm);
			Assert.AreEqual(uz--, ulm--);
			_ = (uz, ulm);
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestShifts()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 100000; i++)
		{
			bytes.FillInPlace(random.Next(259), _ => (byte)random.Next(256));
			MpuT uz = new(bytes.AsSpan(), RandomOrder());
			using UnsignedLongDecimal ulm = new(uz, MantissaLength);
			var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
			uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
			bytes.FillInPlace(random.Next(3), _ => (byte)random.Next(256));
			bytes.PadRightInPlace(4);
			shiftAmount = BitConverter.ToInt32(bytes.AsSpan());
			Assert.AreEqual(uz.ShiftLeftDec(shiftAmount), ulm << shiftAmount);
			Assert.AreEqual(uz.ShiftRightRoundDec(shiftAmount), ulm >> shiftAmount);
			Assert.AreEqual(uz.ShiftRightRoundDec(shiftAmount), ulm >>> shiftAmount);
			Assert.AreEqual(uz.ShiftLeftDec(shiftAmount), ulm << (UnsignedLongDecimal)shiftAmount);
			Assert.AreEqual(uz.ShiftRightRoundDec(shiftAmount), ulm >> (UnsignedLongDecimal)shiftAmount);
			Assert.AreEqual(uz.ShiftRightRoundDec(shiftAmount), ulm >>> (UnsignedLongDecimal)shiftAmount);
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestSubtract()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		var counter = 0;
		List<byte> bytes = new(1024);
		var writeBuffer = GC.AllocateUninitializedArray<byte>(MantissaByteLength * 3);
	l1:
		bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
		MpuT uz = new(bytes.AsSpan(), RandomOrder());
		var mantissaLength = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
		var maxMantissaLength = mantissaLength;
		UnsignedLongDecimal ulm = new(uz, mantissaLength);
		Validate();
		void Action()
		{
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			var mantissaLength2 = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
			MpuT op = new(bytes.AsSpan(), RandomOrder());
			maxMantissaLength = Max(mantissaLength, mantissaLength2);
			var minMantissaLength = Min(mantissaLength, mantissaLength2);
			var shiftAmount = uz.DecLength < minMantissaLength + 1 ? 0
				: Max(uz.DecLength - minMantissaLength - 1, 0);
			var shiftAmountLite = uz.DecLength < minMantissaLength + 1 ? 0
				: Max(uz.DecLength - mantissaLength - 1, 0);
			uz = uz.ShiftRightRoundDec(shiftAmountLite).ShiftLeftDec(shiftAmountLite);
			uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
			ulm = ulm >> shiftAmount << shiftAmount;
			if (random.Next(1000) == 0)
				op = uz;
			if (op > uz)
				return;
			if (uz.DecLength <= op.DecLength + maxMantissaLength)
				uz -= op;
			uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
			ulm -= new UnsignedLongDecimal(op, mantissaLength2);
			Validate();
		}
		for (var i = 0; i < 1000; i++)
		{
			mantissaLength = (int)Round(Pow(2, random.NextDouble() * 2) * 150);
			ulm = new(uz, mantissaLength);
			Action();
		}
		if (counter++ < 10000)
			goto l1;
		int RandomOrder() => random.Next(2) * 2 - 1;
		void Validate()
		{
			var decLengthDiff = uz.DecLength - maxMantissaLength - 1;
			var decLengthDiffClamped = Max(decLengthDiff, 0);
			using var expected = (MpzT)SafeSubtract(uz.ShiftRightRoundDec(decLengthDiffClamped),
				MpuT.PowerOf10(maxMantissaLength), decLengthDiff > 0 || uz.GetByteCount() != ulm.GetByteCount(false));
			Assert.IsTrue(ulm.TryWriteLittleEndian(writeBuffer, out var bytesWritten, false));
			var maxMantissaByteLength = Min(bytesWritten, (int)Ceiling((maxMantissaLength + Log10(9)) * Log(10, 256)));
			using var actual = (MpzT)new MpuT(writeBuffer.AsSpan(0, maxMantissaByteLength), -1);
			Assert.IsLessThanOrEqualTo((MpzT)MpuT.PowerOf10(maxMantissaLength), (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(expected >> 2, (expected - actual).Abs());
			Assert.IsLessThanOrEqualTo(actual >> 2, (expected - actual).Abs());
			if (bytesWritten > maxMantissaByteLength)
				Assert.AreEqual((uint)(decLengthDiffClamped + 1),
					new MpuT(writeBuffer.AsSpan(Min(bytesWritten, maxMantissaByteLength)..bytesWritten), -1));
		}
	}

	[TestMethod]
	public void TestToByteArray()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 1000000; i++)
		{
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			var order = RandomOrder();
			if (order < 0)
				bytes.Resize(Max(bytes.FindLastIndex(x => x != 0), 0) + 1);
			else
				bytes.ResizeLeft(Max(bytes.Length, 1) - Max(bytes.FindIndex(x => x != 0), 0));
			var mantissaLength = random.Next(18, Max((int)Ceiling(bytes.Length * Log10(256)), 18));
			using UnsignedLongDecimal ulm = new(bytes.AsSpan(), order, mantissaLength);
			using UnsignedLongDecimal ulm2 = new(ulm.ToByteArray(order, false), order, mantissaLength);
			Assert.IsTrue(ulm.Equals(ulm2));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestToString()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 2500; i++)
		{
			bytes.FillInPlace(random.Next(MantissaByteLength + 4), _ => (byte)random.Next(256));
			var order = RandomOrder();
			using UnsignedLongDecimal ulm = new(bytes.AsSpan(), order, MantissaLength);
			var @base = (uint)random.Next(2, 37);
			Assert.IsTrue(ulm.Equals(new UnsignedLongDecimal(ulm.ToString())));
			Assert.IsTrue(ulm.Equals(new UnsignedLongDecimal(ulm.ToString(@base), @base)));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestToType()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 10000; i++)
		{
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			MpuT uz = new(bytes.AsSpan(), RandomOrder());
			using UnsignedLongDecimal ulm = new(uz, MantissaLength);
			var shiftAmount = Max(uz.DecLength - MantissaLength - 1, 0);
			uz = uz.ShiftRightRoundDec(shiftAmount).ShiftLeftDec(shiftAmount);
			var type = new[] { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint),
				typeof(long), typeof(ulong), typeof(float), typeof(double), typeof(decimal),
				typeof(MpzT), typeof(MpuT), typeof(string), typeof(object) }.Random(random);
			Assert.AreEqual(((IConvertible)uz).ToType(type, CultureInfo.InvariantCulture),
				((IConvertible)ulm).ToType(type, CultureInfo.InvariantCulture));
			Assert.ThrowsExactly<InvalidCastException>(() =>
				((IConvertible)ulm).ToType(typeof(DateTime), CultureInfo.InvariantCulture));
			Assert.ThrowsExactly<InvalidCastException>(() =>
				((IConvertible)ulm).ToType(typeof(byte[]), CultureInfo.InvariantCulture));
			Assert.AreEqual(((IConvertible)uz).ToType(type, new CultureInfo("ru-RU")),
				((IConvertible)ulm).ToType(type, new CultureInfo("ru-RU")));
			Assert.ThrowsExactly<InvalidCastException>(() =>
				((IConvertible)ulm).ToType(typeof(DateTime), new CultureInfo("ru-RU")));
			Assert.ThrowsExactly<InvalidCastException>(() =>
				((IConvertible)ulm).ToType(typeof(byte[]), new CultureInfo("ru-RU")));
			Assert.AreEqual(((IConvertible)uz).ToType(type, new CultureInfo("zh-Hant-CN")),
				((IConvertible)ulm).ToType(type, new CultureInfo("zh-Hant-CN")));
			Assert.ThrowsExactly<InvalidCastException>(() =>
				((IConvertible)ulm).ToType(typeof(DateTime), new CultureInfo("zh-Hant-CN")));
			Assert.ThrowsExactly<InvalidCastException>(() =>
				((IConvertible)ulm).ToType(typeof(byte[]), new CultureInfo("zh-Hant-CN")));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestTryParse()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024);
		for (var i = 0; i < 5000; i++)
		{
			bytes.FillInPlace(random.Next(MantissaByteLength + 3), _ => (byte)random.Next(256));
			var order = RandomOrder();
			using UnsignedLongDecimal ulm = new(bytes.AsSpan(), order, MantissaLength);
			Assert.IsTrue(UnsignedLongDecimal.TryParse(ulm.ToString(), out var @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse(ulm.ToString(),
				CultureInfo.InvariantCulture, out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse(ulm.ToString(),
				new CultureInfo("ru-RU"), out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse(ulm.ToString(),
				new CultureInfo("zh-Hant-CN"), out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse(ulm.ToString(), NumberStyles.None,
				CultureInfo.InvariantCulture, out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse(ulm.ToString(), NumberStyles.BinaryNumber,
				CultureInfo.InvariantCulture, out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse((ulm.ToString() ?? "0").AsSpan(),
				CultureInfo.InvariantCulture, out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse((ulm.ToString() ?? "0").AsSpan(),
				new CultureInfo("ru-RU"), out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse((ulm.ToString() ?? "0").AsSpan(),
				new CultureInfo("zh-Hant-CN"), out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse((ulm.ToString() ?? "0").AsSpan(), NumberStyles.None,
				CultureInfo.InvariantCulture, out @string) && ulm.Equals(@string));
			Assert.IsTrue(UnsignedLongDecimal.TryParse((ulm.ToString() ?? "0").AsSpan(), NumberStyles.BinaryNumber,
				CultureInfo.InvariantCulture, out @string) && ulm.Equals(@string));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	[TestMethod]
	public void TestTryWrite()
	{
		var random = Lock(lockObj, () => new Random(Global.random.Next()));
		List<byte> bytes = new(1024), bytes2 = new(1024);
		for (var i = 0; i < 250000; i++)
		{
			bytes.FillInPlace(random.Next(500), _ => (byte)random.Next(256));
			var order = RandomOrder();
			var mantissaLength = random.Next(18, Max((int)Ceiling(bytes.Length * Log10(256)), 18));
			using UnsignedLongDecimal ulm = new(bytes.AsSpan(), order, mantissaLength);
			bytes2.FillInPlace(0, bytes.Length + 2);
			if (order < 0)
				Assert.IsTrue(ulm.TryWriteLittleEndian(bytes2.AsSpan(), out _, false));
			else
				Assert.IsTrue(ulm.TryWriteBigEndian(bytes2.AsSpan(), out _, false));
			Assert.IsTrue(ulm.Equals(new UnsignedLongDecimal(bytes2.AsSpan(), order, mantissaLength)));
		}
		int RandomOrder() => random.Next(2) * 2 - 1;
	}

	private static MpuT SafeSubtract(MpuT x, MpuT mantissaOverflow, bool doSubtract) =>
		x >= mantissaOverflow && doSubtract ? x - mantissaOverflow : x;
}
