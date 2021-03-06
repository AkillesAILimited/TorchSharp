// Copyright (c) Microsoft Corporation and contributors.  All Rights Reserved.  See License.txt in the project root for license information.
using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TorchSharp;
using TorchSharp.Tensor;
using Xunit;

#nullable enable

namespace TorchSharp
{
    public class TestTorchTensor
    {
        [Fact]
        public void CreateFloat32TensorOnes()
        {
            var shape = new long[] { 2, 2 };
            TorchTensor t = Float32Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1.0f, t[0, 0].ToSingle());
            Assert.Equal(1.0f, t[1, 1].ToSingle());
        }

        [Fact]
        public void CreateByteTensorOnes()
        {
            var shape = new long[] { 2, 2 };
            TorchTensor t = ByteTensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal((byte)1, t[0,0].ToByte());
            Assert.Equal((byte)1, t[1,1].ToByte());
        }

        [Fact]
        public void CreateInt32TensorOnes()
        {
            var shape = new long[] { 2, 2 };
            TorchTensor t = Int32Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1, t[0,0].ToInt32());
            Assert.Equal(1, t[1,1].ToInt32());
        }

        [Fact]
        public void CreateInt64TensorOnes()
        {
            var shape = new long[] { 2, 2 };

            TorchTensor t = Int64Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1L, t[0,0].ToInt64());
            Assert.Equal(1L, t[1,1].ToInt64());
        }

        [Fact]
        public void CreateBoolTensorOnes()
        {
            var shape = new long[] { 2, 2 };

            TorchTensor t = BoolTensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal((object)true, t[0,0].ToBoolean());
            Assert.Equal((object)true, t[1,1].ToBoolean());
        }

        [Fact]
        public void CreateFloat16TensorOnes()
        {
            foreach (var deviceType in new DeviceType[] { DeviceType.CUDA }) {
                if (deviceType != DeviceType.CUDA || Torch.IsCudaAvailable()) {
                    var shape = new long[] { 2, 2 };

                    TorchTensor t = Float16Tensor.Ones(shape, deviceType: deviceType);
                    Assert.Equal(shape, t.Shape);
                    Assert.Equal(1.0f, t[0, 0].ToSingle());
                    Assert.Equal(1.0f, t[1, 1].ToSingle());
                }
            }
        }

        [Fact]
        public void CreateBFloat16TensorOnes()
        {
            foreach (var deviceType in new DeviceType[] { DeviceType.CUDA }) {
                if (deviceType != DeviceType.CUDA || Torch.IsCudaAvailable()) {
                    var shape = new long[] { 2, 2 };

                    TorchTensor t = BFloat16Tensor.Ones(shape, deviceType: deviceType);
                    Assert.Equal(shape, t.Shape);
                    Assert.Equal(1.0f, t[0, 0].ToSingle());
                    Assert.Equal(1.0f, t[1, 1].ToSingle());
                }
            }
        }

        //[Fact]
        //public void CreateComplexFloat32TensorZeros()
        //{
        //    var shape = new long[] { 2, 2 };

        //    TorchTensor t = ComplexFloat32Tensor.Zeros(shape);
        //    Assert.Equal(shape, t.Shape);
        //    t.ReadComplexFloat(0, out var r3, out var i3);
        //    Assert.Equal(0.0f, r3);
        //    Assert.Equal(0.0f, i3);
        //    t.ReadComplexFloat(3, out var r4, out var i4);
        //    Assert.Equal(0.0f, r4);
        //    Assert.Equal(0.0f, i4);

        //}

        //[Fact]
        //public void CreateComplexFloat32TensorOnes()
        //{
        //    var shape = new long[] { 2, 2 };

        //    TorchTensor t = ComplexFloat32Tensor.Ones(shape);
        //    Assert.Equal(shape, t.Shape);
        //    t.ReadComplexFloat(0, out var r3, out var i3);
        //    Assert.Equal(1.0f, r3);
        //    Assert.Equal(0.0f, i3);
        //    t.ReadComplexFloat(3, out var r4, out var i4);
        //    Assert.Equal(1.0f, r4);
        //    Assert.Equal(0.0f, i4);

        //}

        //[Fact]
        //public void CreateComplexFloat64TensorZeros()
        //{
        //    var shape = new long[] { 2, 2 };

        //    TorchTensor t = ComplexFloat64Tensor.Zeros(shape);
        //    Assert.Equal(shape, t.Shape);
        //    var v3 = t.ReadComplexFloat64(0);
        //    Assert.Equal(0.0, v3.Real);
        //    Assert.Equal(0.0, v3.Imaginary);
        //    var v4 = t.ReadComplexFloat64(3);
        //    Assert.Equal(0.0, v4.Real);
        //    Assert.Equal(0.0, v4.Imaginary);

        //}

        //[Fact]
        //public void CreateComplexFloat64TensorOnes()
        //{
        //    var shape = new long[] { 2, 2 };
        //    TorchTensor t = ComplexFloat64Tensor.Ones(shape);
        //    Assert.Equal(shape, t.Shape);
        //    var v5 = t.ReadComplexFloat64(0);
        //    Assert.Equal(new Complex(1.0, 0.0), v5);
        //    var v6 = t.ReadComplexFloat64(3);
        //    Assert.Equal(new Complex(1.0, 0.0), v6);
        //}

        [Fact]
        public void CreateFloat32TensorCheckMemory()
        {
            TorchTensor? ones = null;

            for (int i = 0; i < 10; i++)
            {
                using (var tmp = Float32Tensor.Ones(new long[] { 100, 100, 100 }))
                {
                    ones = tmp;
                    Assert.NotNull(ones);
                }
            }
        }

        [Fact]
        public void CreateFloat32TensorOnesCheckData()
        {
            var ones = Float32Tensor.Ones(new long[] { 2, 2 });
            var data = ones.Data<float>();

            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(1.0, data[i]);
            }
        }

        [Fact]
        public void CreateFloat32TensorZerosCheckData()
        {
            var zeros = Float32Tensor.Zeros(new long[] { 2, 2 });
            var data = zeros.Data<float>();

            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(0, data[i]);
            }
        }

        [Fact]
        public void CreateInt32TensorOnesCheckData()
        {
            var ones = Int32Tensor.Ones(new long[] { 2, 2 });
            var data = ones.Data<int>();

            for (int i = 0; i < 4; i++)
            {
                Assert.Equal(1, data[i]);
            }
        }

        [Fact]
        public void CreateFloat32TensorCheckDevice()
        {
            var ones = Float32Tensor.Ones(new long[] { 2, 2 });
            var device = ones.DeviceString;

            Assert.Equal("cpu", ones.DeviceString);
        }

        [Fact]
        public void CreateFloat32TensorFromData()
        {
            var data = new float[1000];
            data[100] = 1;

            using (var tensor = Float32Tensor.From(data, new long[] { 100, 10 }))
            {
                Assert.Equal(1, tensor.Data<float>()[100]);
            }
        }

        [Fact]
        public void CreateFloat32TensorFromDataCheckDispose()
        {
            var data = new float[1000];
            data[100] = 1;

            using (var tensor = Float32Tensor.From(data, new long[] { 100, 10 }))
            {
                Assert.Equal(1, tensor.Data<float>()[100]);
            }

            Assert.Equal(1, data[100]);
        }

        [Fact]
        public void CreateFloat32TensorFromData2()
        {
            var data = new float[1000];

            using (var tensor = data.ToTorchTensor(new long[] { 10, 100 })) {
                Assert.Equal(default(float), tensor.Data<float>()[100]);
            }
        }

        [Fact]
        public void CreateFloat32TensorFromDataCheckStrides()
        {
            var data = new double[] { 0.2663158, 0.1144736, 0.1147367, 0.1249998, 0.1957895, 0.1231576, 0.1944732, 0.111842, 0.1065789, 0.667881, 0.5682123, 0.5824502, 0.4824504, 0.4844371, 0.6463582, 0.5334439, 0.5079474, 0.2281452 };
            var dataTensor = data.ToTorchTensor(new long[] { 2, 9 });

            for (int r = 0; r < 2; r++)
            {
                for (int i = 0; i < 9; i++)
                {
                    var fromData = data[(r * 9) + i];
                    var fromTensor = dataTensor[r, i].ToDouble();
                    Assert.True(Math.Abs(fromData - fromTensor) < 0.0001);
                }
            }

            var firstPart = dataTensor[0];

            for (int i = 0; i < 9; i++)
            {
                var fromData = data[i];
                var fromChunk = firstPart[i].ToDouble();
                Assert.True(Math.Abs(fromData - fromChunk) < 0.0001);
            }
        }


        [Fact]
        public void CreateFloat16TensorFromDataCheckStrides()
        {
            var data = new float[] { 0.2663158f, 0.1144736f, 0.1147367f, 0.1249998f, 0.1957895f, 0.1231576f, 0.1944732f, 0.111842f, 0.1065789f, 0.667881f, 0.5682123f, 0.5824502f, 0.4824504f, 0.4844371f, 0.6463582f, 0.5334439f, 0.5079474f, 0.2281452f };
            var dataTensor = Float16Tensor.From(data, new long[] { 2, 9 });

            for (int r = 0; r < 2; r++) {
                for (int i = 0; i < 9; i++) {
                    var fromData = data[(r * 9) + i];
                    var fromTensor = dataTensor[r, i].ToSingle();
                    Assert.True(Math.Abs(fromData - fromTensor) < 0.01);
                }
            }

            var firstPart = dataTensor[0];

            for (int i = 0; i < 9; i++) {
                var fromData = data[i];
                var fromChunk = firstPart[i].ToSingle();
                Assert.True(Math.Abs(fromData - fromChunk) < 0.01);
            }
        }

        [Fact]
        public void CreateBFloat16TensorFromDataCheckStrides()
        {
            var data = new float[] { 0.2663158f, 0.1144736f, 0.1147367f, 0.1249998f, 0.1957895f, 0.1231576f, 0.1944732f, 0.111842f, 0.1065789f, 0.667881f, 0.5682123f, 0.5824502f, 0.4824504f, 0.4844371f, 0.6463582f, 0.5334439f, 0.5079474f, 0.2281452f };
            var dataTensor = BFloat16Tensor.From(data, new long[] { 2, 9 });

            for (int r = 0; r < 2; r++) {
                for (int i = 0; i < 9; i++) {
                    var fromData = data[(r * 9) + i];
                    var fromTensor = dataTensor[r,i].ToSingle();
                    Assert.True(Math.Abs(fromData - fromTensor) < 0.1);
                }
            }

            var firstPart = dataTensor[0];

            for (int i = 0; i < 9; i++) {
                var fromData = data[i];
                var fromChunk = firstPart[i].ToSingle();
                Assert.True(Math.Abs(fromData - fromChunk) < 0.1);
            }
        }


        [Fact]
        public void CreateFloat32TensorFromScalar()
        {
            float scalar = 333.0f;

            using (var tensor = Float32Tensor.From(scalar))
            {
                Assert.Equal(333.0f, tensor.ToSingle());
            }
        }

        [Fact]
        public void CreateFloat16TensorFromScalar()
        {
            float scalar = 333.0f;

            using (var tensor = Float16Tensor.From(scalar)) {
                Assert.Equal(333.0f, tensor.ToSingle());
            }
        }

        [Fact]
        public void CreateBFloat16TensorFromScalar()
        {
            float scalar = 333.0f;

            using (var tensor = BFloat16Tensor.From(scalar)) {
                Assert.Equal(332.0f, tensor.ToSingle()); // NOTE: bfloat16 loses precision, this really is 332.0f
            }
        }

        [Fact]
        public void CreateFloat32TensorFromScalar2()
        {
            float scalar = 333.0f;

            using (var tensor = scalar.ToTorchTensor())
            {
                Assert.Equal(333, tensor.ToSingle());
            }
        }

        [Fact]
        public void GetSetItem2()
        {
            var shape = new long[] { 2, 3 };
            TorchTensor t = Float32Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1.0f, t[0, 0].ToSingle());
            Assert.Equal(1.0f, t[1, 2].ToSingle());
            t[1, 2] = Float32Tensor.From(2.0f);
            Assert.Equal(2.0f, t[1, 2].ToSingle());
        }

        [Fact]
        public void GetSetItem3()
        {
            var shape = new long[] { 2, 3, 4 };
            TorchTensor t = Float32Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1.0f, t[0, 0, 0].ToSingle());
            Assert.Equal(1.0f, t[1, 2, 3].ToSingle());
            t[1, 2, 3] = Float32Tensor.From(2.0f);
            Assert.Equal(2.0f, t[1, 2, 3].ToSingle());
        }

        [Fact]
        public void GetSetItem4()
        {
            var shape = new long[] { 2, 3, 4, 5 };
            TorchTensor t = Float32Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1.0f, t[0, 0, 0, 0].ToSingle());
            Assert.Equal(1.0f, t[1, 2, 3, 4].ToSingle());
            t[1, 2, 3, 4] = Float32Tensor.From(2.0f);
            Assert.Equal(2.0f, t[1, 2, 3, 4].ToSingle());
        }

        [Fact]
        public void GetSetItem5()
        {
            var shape = new long[] { 2, 3, 4, 5, 6 };
            TorchTensor t = Float32Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1.0f, t[0, 0, 0, 0, 0].ToSingle());
            Assert.Equal(1.0f, t[1, 2, 3, 4, 5].ToSingle());
            t[1, 2, 3, 4, 5] = Float32Tensor.From(2.0f);
            Assert.Equal(2.0f, t[1, 2, 3, 4, 5].ToSingle());
        }


        [Fact]
        public void GetSetItem6()
        {
            var shape = new long[] { 2, 3, 4, 5, 6, 7 };
            TorchTensor t = Float32Tensor.Ones(shape);
            Assert.Equal(shape, t.Shape);
            Assert.Equal(1.0f, t[0, 0, 0, 0, 0, 0].ToSingle());
            Assert.Equal(1.0f, t[1, 2, 3, 4, 5, 6].ToSingle());
            t[1, 2, 3, 4, 5, 6] = Float32Tensor.From(2.0f);
            Assert.Equal(2.0f, t[1, 2, 3, 4, 5, 6].ToSingle());
        }

        [Fact]
        public void TestScalarToTensor()
        {
            Assert.Throws<ArgumentException>(() => 1.ToTorchTensor(requiresGrad: true));
        }

        [Fact]
        public void TestScalarToTensor2()
        {
            using (var tensor = 1.ToTorchTensor())
            {
                Assert.Equal(ScalarType.Int32, tensor.Type);
                Assert.Equal(1, tensor.ToInt32());
            }
            using (var tensor = ((byte)1).ToTorchTensor())
            {
                Assert.Equal(ScalarType.Byte, tensor.Type);
                Assert.Equal(1, tensor.ToByte());
            }
            using (var tensor = ((sbyte)-1).ToTorchTensor())
            {
                Assert.Equal(ScalarType.Int8, tensor.Type);
                Assert.Equal(-1, tensor.ToSByte());
            }
            using (var tensor = ((short)-1).ToTorchTensor())
            {
                Assert.Equal(ScalarType.Int16, tensor.Type);
                Assert.Equal(-1, tensor.ToInt16());
            }
            using (var tensor = ((long)-1).ToTorchTensor())
            {
                Assert.Equal(ScalarType.Int64, tensor.Type);
                Assert.Equal(-1L, tensor.ToInt64());
            }
            using (var tensor = ((float)-1).ToTorchTensor())
            {
                Assert.Equal(ScalarType.Float32, tensor.Type);
                Assert.Equal(-1.0f, tensor.ToSingle());
            }
            using (var tensor = ((double)-1).ToTorchTensor())
            {
                Assert.Equal(ScalarType.Float64, tensor.Type);
                Assert.Equal(-1.0, tensor.ToDouble());
            }
        }

        [Fact]
        public void TestScalarToTensor3()
        {
            using (var tensor = 1.ToTorchTensor()) {
                Assert.Equal(ScalarType.Int32, tensor.Type);
                Assert.Equal(1, (int) tensor);
            }
            using (var tensor = ((byte)1).ToTorchTensor()) {
                Assert.Equal(ScalarType.Byte, tensor.Type);
                Assert.Equal(1, (byte) tensor);
            }
            using (var tensor = ((sbyte)-1).ToTorchTensor()) {
                Assert.Equal(ScalarType.Int8, tensor.Type);
                Assert.Equal(-1, (sbyte) tensor);
            }
            using (var tensor = ((short)-1).ToTorchTensor()) {
                Assert.Equal(ScalarType.Int16, tensor.Type);
                Assert.Equal(-1, (short) tensor);
            }
            using (var tensor = ((long)-1).ToTorchTensor()) {
                Assert.Equal(ScalarType.Int64, tensor.Type);
                Assert.Equal(-1L, (long) tensor);
            }
            using (var tensor = ((float)-1).ToTorchTensor()) {
                Assert.Equal(ScalarType.Float32, tensor.Type);
                Assert.Equal(-1.0f, (float) tensor);
            }
            using (var tensor = ((double)-1).ToTorchTensor()) {
                Assert.Equal(ScalarType.Float64, tensor.Type);
                Assert.Equal(-1.0, (double) tensor);
            }
        }

        [Fact]
        public void InitUniform()
        {
            using (TorchTensor tensor = Float32Tensor.Zeros(new long[] { 2, 2 }))
            {
                NN.Init.Uniform(tensor);
            }
        }

        [Fact]
        public void TestSparse()
        {
            using (var i = Int64Tensor.From(new long[] { 0, 1, 1, 2, 0, 2 }, new long[] { 2, 3 }))
            using (var v = Float32Tensor.From(new float[] { 3, 4, 5 }, new long[] { 3 }))
            {
                var sparse = Float32Tensor.Sparse(i, v, new long[] { 2, 3 });

                Assert.True(sparse.IsSparse);
                Assert.False(i.IsSparse);
                Assert.False(v.IsSparse);
                Assert.Equal(sparse.SparseIndices.Data<long>().ToArray(), new long[] { 0, 1, 1, 2, 0, 2 });
                Assert.Equal(sparse.SparseValues.Data<float>().ToArray(), new float[] { 3, 4, 5 });
            }
        }

        [Fact]
        public void CopyCpuToCuda()
        {
            TorchTensor cpu = Float32Tensor.Ones(new long[] { 2, 2 });
            Assert.Equal("cpu", cpu.DeviceString);

            if (Torch.IsCudaAvailable())
            {
                var cuda = cpu.Cuda();
                Assert.Equal("cuda:0", cuda.DeviceString);

                // Copy back to CPU to inspect the elements
                var cpu2 = cuda.Cpu();
                Assert.Equal("cpu", cpu2.DeviceString);
                var data = cpu.Data<float>();
                for (int i = 0; i < 4; i++)
                {
                    Assert.Equal(1, data[i]);
                }
            }
            else
            {
                Assert.Throws<InvalidOperationException>(() => cpu.Cuda());
            }

        }

        [Fact]
        public void CopyCudaToCpu()
        {
            if (Torch.IsCudaAvailable())
            {
                var cuda = Float32Tensor.Ones(new long[] { 2, 2 }, DeviceType.CUDA);
                Assert.Equal("cuda:0", cuda.DeviceString);

                var cpu = cuda.Cpu();
                Assert.Equal("cpu", cpu.DeviceString);

                var data = cpu.Data<float>();
                for (int i = 0; i < 4; i++)
                {
                    Assert.Equal(1, data[i]);
                }
            }
            else
            {
                Assert.Throws<InvalidOperationException>(() => { Float32Tensor.Ones(new long[] { 2, 2 }, DeviceType.CUDA); });
            }
        }

        [Fact]
        public void TestSquareEuclideanDistance()
        {
            var input = new double[] { 0.1, 0.1, 0.1, 0.1, 0.2, 0.1, 0.2, 0.1, 0.1 }.ToTorchTensor(new long[] { 9 }).ToType(ScalarType.Float32);
            var zeros = Float32Tensor.Zeros(new long[] { 1, 9 });
            var ones = Float32Tensor.Ones(new long[] { 1, 9 });
            var centroids = new TorchTensor[] { zeros, ones }.Cat(0);

            var distanceFromZero = input.Reshape(new long[] { -1, 1, 9 }).Sub(zeros).Pow(2.ToScalar()).Sum(new long[] { 2 });
            var distanceFromOne = input.Reshape(new long[] { -1, 1, 9 }).Sub(ones).Pow(2.ToScalar()).Sum(new long[] { 2 });
            var distanceFromCentroids = input.Reshape(new long[] { -1, 1, 9 }).Sub(centroids).Pow(2.ToScalar()).Sum(new long[] { 2 });

            Assert.True(true);
        }

        [Fact]
        public void TestCat()
        {
            var zeros = Float32Tensor.Zeros(new long[] { 1, 9 });
            var ones = Float32Tensor.Ones(new long[] { 1, 9 });
            var centroids = new TorchTensor[] { zeros, ones }.Cat(0);

            var shape = centroids.Shape;
            Assert.Equal(new long[] { 2, 9 }, shape);
        }

        [Fact]
        public void TestCatCuda()
        {
            if (Torch.IsCudaAvailable()) {
                var zeros = Float32Tensor.Zeros(new long[] { 1, 9 }).Cuda();
                var ones = Float32Tensor.Ones(new long[] { 1, 9 }).Cuda();
                var centroids = new TorchTensor[] { zeros, ones }.Cat(0);
                var shape = centroids.Shape;
                Assert.Equal(new long[] { 2, 9 }, shape);
                Assert.Equal(DeviceType.CUDA, centroids.DeviceType);
            }
        }

        void TestStackGen(DeviceType device)
        {
            {
                var t1 = Float32Tensor.Zeros( new long[] { }, device );
                var t2 = Float32Tensor.Ones(new long[] { }, device);
                var t3 = Float32Tensor.Ones(new long[] { }, device);
                var res = new TorchTensor[] { t1, t2, t3 }.Stack(0);

                var shape = res.Shape;
                Assert.Equal(new long[] { 3 }, shape);
                Assert.Equal(device, res.DeviceType);
            }
            {
                var t1 = Float32Tensor.Zeros(new long[] { 2, 9 }, device);
                var t2 = Float32Tensor.Ones(new long[] { 2, 9 }, device);
                var res = new TorchTensor[] { t1, t2 }.Stack(0);

                var shape = res.Shape;
                Assert.Equal(new long[] { 2, 2, 9 }, shape);
                Assert.Equal(device, res.DeviceType);
            }
        }

        [Fact]
        public void TestStackCpu()
        {
            TestStackGen(DeviceType.CPU);
        }

        [Fact]
        public void TestStackCuda()
        {
            if (Torch.IsCudaAvailable()) {
                TestStackGen(DeviceType.CUDA);
            }
        }

        [Fact]
        public void TestSetGrad()
        {
            var x = Float32Tensor.Random(new long[] { 10, 10 });
            Assert.False(x.IsGradRequired);

            x.RequiresGrad(true);
            Assert.True(x.IsGradRequired);
            x.RequiresGrad(false);
            Assert.False(x.IsGradRequired);
        }

        [Fact(Skip = "Not working on MacOS (note: may now be working, we need to recheck)")]
        public void TestAutoGradMode()
        {
            var x = Float32Tensor.RandomN(new long[] { 2, 3 }, requiresGrad: true);
            using (var mode = new AutoGradMode(false))
            {
                Assert.False(AutoGradMode.IsAutogradEnabled());
                var sum = x.Sum();
                Assert.Throws<ExternalException>(() => sum.Backward());
                //var grad = x.Grad();
                //Assert.True(grad.Handle == IntPtr.Zero);
            }
            using (var mode = new AutoGradMode(true))
            {
                Assert.True(AutoGradMode.IsAutogradEnabled());
                var sum = x.Sum();
                sum.Backward();
                var grad = x.Grad();
                Assert.False(grad.Handle == IntPtr.Zero);
                var data = grad.Data<float>();
                for (int i = 0; i < 2 * 3; i++)
                {
                    Assert.Equal(1.0, data[i]);
                }
            }
        }

        [Fact]
        public void TestSubInPlace()
        {
            var x = Int32Tensor.Ones(new long[] { 100, 100 });
            var y = Int32Tensor.Ones(new long[] { 100, 100 });

            x.SubInPlace(y);

            var xdata = x.Data<int>();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Assert.Equal(0, xdata[i + j]);
                }
            }
        }

        [Fact]
        public void TestMemoryDisposalZeros()
        {
            for (int i = 0; i < 1024; i++) {
                var x = Float64Tensor.Zeros(new long[] { 1024, 1024 });
                x.Dispose();
                //System.GC.Collect();
            }
        }

        [Fact]
        public void TestMemoryDisposalOnes()
        {
            for (int i = 0; i < 1024; i++) {
                var x = Float64Tensor.Ones(new long[] { 1024, 1024 });
                x.Dispose();
            }
        }

        [Fact]
        public void TestMemoryDisposalScalarTensors()
        {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 1000 * 100; j++) {
                    var x = Float64Tensor.From(i * j * 3.1415);
                    x.Dispose();
                }
                //System.GC.Collect();
            }
        }

        [Fact]
        public void TestMemoryDisposalScalars()
        {
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 1000 * 100; j++) {
                    var x = (i * j * 3.1415).ToScalar();
                    x.Dispose();
                }
                //System.GC.Collect();
            }
        }


        [Fact]
        public void TestSaveLoadTensorDouble()
        {
            var file = ".saveload.double.ts";
            if (File.Exists(file)) File.Delete(file);
            var tensor = Float64Tensor.Ones(new long[] { 5, 6 });
            tensor.Save(file);
            var tensorLoaded = TorchTensor.Load(file);
            File.Delete(file);
            Assert.NotNull(tensorLoaded);
            Assert.Equal(tensorLoaded.Type, tensor.Type);
            Assert.Equal(tensorLoaded, tensor);
        }

        [Fact]
        public void TestSaveLoadTensorFloat()
        {
            var file = ".saveload.float.ts";
            if (File.Exists(file)) File.Delete(file);
            var tensor = Float32Tensor.Ones(new long[] { 5, 6 });
            tensor.Save(file);
            var tensorLoaded = TorchTensor.Load(file);
            File.Delete(file);
            Assert.NotNull(tensorLoaded);
            Assert.Equal(tensorLoaded.Type, tensor.Type);
            Assert.Equal(tensorLoaded, tensor);
        }


        [Fact]
        public void TestArithmeticOperatorsFloat16()
        {
            // Float16 arange_cuda not available on cuda in LibTorch 1.7.0
            // Float16 arange_cpu not available on cuda in LibTorch 1.7.0
            foreach (var deviceType in new DeviceType[] { DeviceType.CPU, DeviceType.CUDA }) {
                if (deviceType != DeviceType.CUDA || Torch.IsCudaAvailable()) {
                    var c1 = Float16Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    var c2 = Float16Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    var c3 = Float16Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    Func<TorchTensor, long, long, float> getFunc = (tt, i, j) => tt[i,j].ToSingle();
                    // scalar-tensor operators
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a + 0.5f, a => a + 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => 0.5f + a, a => 0.5f + a);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a - 0.5f, a => a - 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a * 0.5f, a => a * 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => 0.5f * a, a => 0.5f * a);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a / 0.5f, a => a / 0.5f);

                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Add(0.5f), a => a + 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Sub(0.5f), a => a - 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Mul(0.5f), a => a * 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Div(0.5f), a => a / 0.5f);

                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.AddInPlace(0.5f), a => a + 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.SubInPlace(0.5f), a => a - 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.MulInPlace(0.5f), a => a * 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.DivInPlace(0.5f), a => a / 0.5f);

                    // tensor-tensor operators
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a + b, (a, b) => a + b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a - b, (a, b) => a - b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a * b, (a, b) => a * b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a / b, (a, b) => a / b);

                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Add(b), (a, b) => a + b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Sub(b), (a, b) => a - b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Mul(b), (a, b) => a * b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Div(b), (a, b) => a / b);

                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.AddInPlace(b), (a, b) => a + b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.SubInPlace(b), (a, b) => a - b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.MulInPlace(b), (a, b) => a * b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.DivInPlace(b), (a, b) => a / b);
                }
            }
        }

        [Fact]
        public void TestArithmeticOperatorsBFloat16()
        {
            // BFloat16 arange_cuda not available on cuda in LibTorch 1.7.0
            // BFloat16 arange_cpu not available on cuda in LibTorch 1.7.0
            foreach (var deviceType in new DeviceType[] { DeviceType.CPU, DeviceType.CUDA }) {
                if (deviceType != DeviceType.CUDA || Torch.IsCudaAvailable()) {
                    var c1 = BFloat16Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    var c2 = BFloat16Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    var c3 = BFloat16Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    Func<TorchTensor, long, long, float> getFunc = (tt, i, j) => tt[i,j].ToSingle();
                    // scalar-tensor operators
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a + 0.5f, a => a + 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => 0.5f + a, a => 0.5f + a);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a - 0.5f, a => a - 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a * 0.5f, a => a * 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => 0.5f * a, a => 0.5f * a);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a / 0.5f, a => a / 0.5f);

                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Add(0.5f), a => a + 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Sub(0.5f), a => a - 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Mul(0.5f), a => a * 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Div(0.5f), a => a / 0.5f);

                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.AddInPlace(0.5f), a => a + 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.SubInPlace(0.5f), a => a - 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.MulInPlace(0.5f), a => a * 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.DivInPlace(0.5f), a => a / 0.5f);

                    // tensor-tensor operators
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a + b, (a, b) => a + b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a - b, (a, b) => a - b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a * b, (a, b) => a * b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a / b, (a, b) => a / b);

                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Add(b), (a, b) => a + b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Sub(b), (a, b) => a - b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Mul(b), (a, b) => a * b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Div(b), (a, b) => a / b);

                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.AddInPlace(b), (a, b) => a + b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.SubInPlace(b), (a, b) => a - b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.MulInPlace(b), (a, b) => a * b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.DivInPlace(b), (a, b) => a / b);
                }
            }
        }

        [Fact]
        public void TestArithmeticOperatorsFloat32()
        {
            foreach (var deviceType in new DeviceType[] { DeviceType.CPU, DeviceType.CUDA }) {
                if (deviceType != DeviceType.CUDA || Torch.IsCudaAvailable()) {
                    var c1 = Float32Tensor.Arange(0, 10, 1, deviceType: deviceType).Expand(new long[] { 10, 10 });
                    var c2 = Float32Tensor.Arange(10, 0, -1, deviceType: deviceType).Expand(new long[] { 10, 10 });
                    var c3 = Float32Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    Func<TorchTensor, long, long, float> getFunc = (tt, i, j) => tt[i,j].ToSingle();
                    // scalar-tensor operators
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a + 0.5f, a => a + 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => 0.5f + a, a => 0.5f + a);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a - 0.5f, a => a - 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a * 0.5f, a => a * 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => 0.5f * a, a => 0.5f * a);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a / 0.5f, a => a / 0.5f);

                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Add(0.5f), a => a + 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Sub(0.5f), a => a - 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Mul(0.5f), a => a * 0.5f);
                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a.Div(0.5f), a => a / 0.5f);

                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.AddInPlace(0.5f), a => a + 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.SubInPlace(0.5f), a => a - 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.MulInPlace(0.5f), a => a * 0.5f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.DivInPlace(0.5f), a => a / 0.5f);

                    // tensor-tensor operators
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a + b, (a, b) => a + b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a - b, (a, b) => a - b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a * b, (a, b) => a * b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a / b, (a, b) => a / b);

                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Add(b), (a, b) => a + b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Sub(b), (a, b) => a - b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Mul(b), (a, b) => a * b);
                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Div(b), (a, b) => a / b);

                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.AddInPlace(b), (a, b) => a + b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.SubInPlace(b), (a, b) => a - b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.MulInPlace(b), (a, b) => a * b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.DivInPlace(b), (a, b) => a / b);
                }
            }
        }

        [Fact]
        public void TestArithmeticOperatorsFloat64()
        {
            foreach (var deviceType in new DeviceType[] { DeviceType.CPU, DeviceType.CUDA }) {
                if (deviceType != DeviceType.CUDA || Torch.IsCudaAvailable()) {
                    var c1 = Float64Tensor.Arange(0, 10, 1, deviceType: deviceType).Expand(new long[] { 10, 10 });
                    var c2 = Float64Tensor.Arange(10, 0, -1, deviceType: deviceType).Expand(new long[] { 10, 10 });
                    var c3 = Float64Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    Func<TorchTensor, long, long, double> getFunc = (tt, i, j) => tt[i, j].ToDouble(); 
                    // scalar-tensor operators
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a + 0.5, a => a + 0.5);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => 0.5 + a, a => 0.5 + a);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a - 0.5, a => a - 0.5);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a * 0.5, a => a * 0.5);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => 0.5 * a, a => 0.5 * a);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a / 0.5, a => a / 0.5);

                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a.Add(0.5), a => a + 0.5);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a.Sub(0.5), a => a - 0.5);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a.Mul(0.5), a => a * 0.5);
                    TestOneTensor<double, double>(c1, c2, getFunc, getFunc, a => a.Div(0.5), a => a / 0.5);

                    TestOneTensorInPlace<double>(c1, c2, getFunc, a => a.AddInPlace(0.5), a => a + 0.5);
                    TestOneTensorInPlace<double>(c1, c2, getFunc, a => a.SubInPlace(0.5), a => a - 0.5);
                    TestOneTensorInPlace<double>(c1, c2, getFunc, a => a.MulInPlace(0.5), a => a * 0.5);
                    TestOneTensorInPlace<double>(c1, c2, getFunc, a => a.DivInPlace(0.5), a => a / 0.5);

                    // tensor-tensor operators
                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a + b, (a, b) => a + b);
                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a - b, (a, b) => a - b);
                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a * b, (a, b) => a * b);
                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a / b, (a, b) => a / b);

                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Add(b), (a, b) => a + b);
                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Sub(b), (a, b) => a - b);
                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Mul(b), (a, b) => a * b);
                    TestTwoTensor<double, double>(c1, c2, c3, getFunc, getFunc, (a, b) => a.Div(b), (a, b) => a / b);

                    TestTwoTensorInPlace<double>(c1, c2, c3, getFunc, (a, b) => a.AddInPlace(b), (a, b) => a + b);
                    TestTwoTensorInPlace<double>(c1, c2, c3, getFunc, (a, b) => a.SubInPlace(b), (a, b) => a - b);
                    TestTwoTensorInPlace<double>(c1, c2, c3, getFunc, (a, b) => a.MulInPlace(b), (a, b) => a * b);
                    TestTwoTensorInPlace<double>(c1, c2, c3, getFunc, (a, b) => a.DivInPlace(b), (a, b) => a / b);
                }
            }
        }

        [Fact]
        public void TestComparisonOperatorsFloat32()
        {
            foreach (var deviceType in new DeviceType[] { DeviceType.CPU, DeviceType.CUDA }) {
                if (deviceType != DeviceType.CUDA || Torch.IsCudaAvailable()) {
                    var c1 =Float32Tensor.Arange(0, 10, 1, deviceType: deviceType).Expand(new long[] { 10, 10 });
                    var c2 = Float32Tensor.Arange(10, 0, -1, deviceType: deviceType).Expand(new long[] { 10, 10 });
                    var c3 = Float32Tensor.Ones(new long[] { 10, 10 }, deviceType: deviceType);
                    Func<TorchTensor, long, long, float> getFunc = (tt, i, j) => tt[i, j].ToSingle(); 
                    Func<TorchTensor, long, long, bool> getFuncBool = (tt, i, j) => tt[i, j].ToBoolean(); 
                    // scalar-tensor operators
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => a == 5.0f, a => a == 5.0f);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => a != 5.0f, a => a != 5.0f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.EqInPlace(5.0f), a => a == 5.0f ? 1.0f : 0.0f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.NeInPlace(5.0f), a => a != 5.0f ? 1.0f : 0.0f);

                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => a < 5.0f, a => a < 5.0f);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => 5.0f < a, a => 5.0f < a);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => a <= 5.0f, a => a <= 5.0f);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => 5.0f <= a, a => 5.0f <= a);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => a > 5.0f, a => a > 5.0f);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => 5.0f > a, a => 5.0f > a);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => a >= 5.0f, a => a >= 5.0f);
                    TestOneTensor<float, bool>(c1, c2, getFunc, getFuncBool, a => 5.0f >= a, a => 5.0f >= a);

                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.LtInPlace(5.0f), a => a < 5.0f ? 1.0f : 0.0f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.LeInPlace(5.0f), a => a <= 5.0f ? 1.0f : 0.0f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.GtInPlace(5.0f), a => a > 5.0f ? 1.0f : 0.0f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.GeInPlace(5.0f), a => a >= 5.0f ? 1.0f : 0.0f);

                    TestOneTensor<float, float>(c1, c2, getFunc, getFunc, a => a % 5.0f, a => a % 5.0f);
                    TestOneTensorInPlace<float>(c1, c2, getFunc, a => a.RemainderInPlace(5.0f), a => a % 5.0f);

                    // tensor-tensor operators
                    TestTwoTensor<float, bool>(c1, c2, c3, getFunc, getFuncBool, (a, b) => a == b, (a, b) => a == b);
                    TestTwoTensor<float, bool>(c1, c2, c3, getFunc, getFuncBool, (a, b) => a != b, (a, b) => a != b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.EqInPlace(b), (a, b) => a == b ? 1.0f : 0.0f);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.NeInPlace(b), (a, b) => a != b ? 1.0f : 0.0f);

                    TestTwoTensor<float, bool>(c1, c2, c3, getFunc, getFuncBool, (a, b) => a < b, (a, b) => a < b);
                    TestTwoTensor<float, bool>(c1, c2, c3, getFunc, getFuncBool, (a, b) => a <= b, (a, b) => a <= b);
                    TestTwoTensor<float, bool>(c1, c2, c3, getFunc, getFuncBool, (a, b) => a > b, (a, b) => a > b);
                    TestTwoTensor<float, bool>(c1, c2, c3, getFunc, getFuncBool, (a, b) => a >= b, (a, b) => a >= b);

                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.LtInPlace(b), (a, b) => a < b ? 1.0f : 0.0f);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.LeInPlace(b), (a, b) => a <= b ? 1.0f : 0.0f);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.GtInPlace(b), (a, b) => a > b ? 1.0f : 0.0f);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.GeInPlace(b), (a, b) => a >= b ? 1.0f : 0.0f);

                    TestTwoTensor<float, float>(c1, c2, c3, getFunc, getFunc, (a, b) => a % b, (a, b) => a % b);
                    TestTwoTensorInPlace<float>(c1, c2, c3, getFunc, (a, b) => a.RemainderInPlace(b), (a, b) => a % b);
                }
            }
        }

        private void TestOneTensor<Tin, Tout>(
            TorchTensor c1,
            TorchTensor c2,
            Func<TorchTensor, long, long, Tin> getFuncIn,
            Func<TorchTensor, long, long, Tout> getFuncOut,
            Func<TorchTensor, TorchTensor> tensorFunc,
            Func<Tin, Tout> scalarFunc)
        {
            var x = c1 * c2;
            var y = tensorFunc(x);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var xv = getFuncIn(x, i, j);
                    var yv = getFuncOut(y, i, j);
                    Assert.Equal<Tout>(yv, scalarFunc(xv));
                }
            }
        }

        private void TestOneTensorInPlace<Tin>(
            TorchTensor c1,
            TorchTensor c2,
            Func<TorchTensor, long, long, Tin> getFuncIn,
            Func<TorchTensor, TorchTensor> tensorFunc,
            Func<Tin, Tin> scalarFunc)
        {

            var x = c1 * c2;
            var xClone = x.Clone();
            var y = tensorFunc(x);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var xClonev = getFuncIn(xClone, i, j);
                    var xv = getFuncIn(x, i, j);
                    var yv = getFuncIn(y, i, j);
                    Assert.Equal(yv, scalarFunc(xClonev));
                    Assert.Equal(yv, xv);
                }
            }
        }

        private void TestTwoTensor<Tin, Tout>(
            TorchTensor c1,
            TorchTensor c2,
            TorchTensor c3,
            Func<TorchTensor, long, long, Tin> getFuncIn,
            Func<TorchTensor, long, long, Tout> getFuncOut,
            Func<TorchTensor, TorchTensor, TorchTensor> tensorFunc,
            Func<Tin, Tin, Tout> scalarFunc)
        {

            var x = c1 * c3;
            var y = c2 * c3;

            var z = tensorFunc(x, y);

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var xv = getFuncIn(x, i, j);
                    var yv = getFuncIn(y, i, j);
                    var zv = getFuncOut(z, i, j);
                    Assert.Equal(zv, scalarFunc(xv, yv));
                }
            }
        }

        private void TestTwoTensorInPlace<Tin>(
            TorchTensor c1,
            TorchTensor c2,
            TorchTensor c3,
            Func<TorchTensor, long, long, Tin> getFuncIn,
            Func<TorchTensor, TorchTensor, TorchTensor> tensorFunc,
            Func<Tin, Tin, Tin> scalarFunc)
        {

            var x = c1 * c3;
            var xClone = x.Clone();
            var y = c2 * c3;

            var z = tensorFunc(x, y);

            var xData = x.Data<Tin>();
            var xCloneData = xClone.Data<Tin>();
            var yData = y.Data<Tin>();
            var zData = z.Data<Tin>();

            Assert.True(xData == zData);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var xClonev = getFuncIn(xClone, i, j);
                    var xv = getFuncIn(x, i, j);
                    var yv = getFuncIn(y, i, j);
                    var zv = getFuncIn(z, i, j);
                    Assert.Equal(zv, scalarFunc(xClonev, yv));
                    Assert.Equal(zv, xv);
                }
            }
        }

        [Fact]
        public void TestMul()
        {
            var x = Float32Tensor.Ones(new long[] { 100, 100 });

            var y = x.Mul(0.5f.ToScalar());

            var ydata = y.Data<float>();
            var xdata = x.Data<float>();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    Assert.Equal(ydata[i + j], xdata[i + j] * 0.5f);
                }
            }
        }

        void TestMmGen(DeviceType device)
        {
            {
                var x1 = Float32Tensor.Ones(new long[] { 1, 2 }, deviceType: device);
                var x2 = Float32Tensor.Ones(new long[] { 2, 1 }, deviceType: device);

                var y = x1.Mm(x2).ToDevice(DeviceType.CPU);

                var ydata = y.Data<float>();

                Assert.Equal(2.0f, ydata[0]);
            }
            //System.Runtime.InteropServices.ExternalException : addmm for CUDA tensors only supports floating - point types.Try converting the tensors with.float() at C:\w\b\windows\pytorch\aten\src\THC / generic / THCTensorMathBlas.cu:453
            if (device == DeviceType.CPU) {
                var x1 = Int64Tensor.Ones(new long[] { 1, 2 }, deviceType: device);
                var x2 = Int64Tensor.Ones(new long[] { 2, 1 }, deviceType: device);

                var y = x1.Mm(x2).ToDevice(DeviceType.CPU);

                var ydata = y.Data<long>();

                Assert.Equal(2L, ydata[0]);
            }
        }

        [Fact]
        public void TestMmCpu()
        {
            TestMmGen(DeviceType.CPU);
        }

        [Fact]
        public void TestMmCuda()
        {
            if (Torch.IsCudaAvailable()) {
                TestMmGen(DeviceType.CUDA);
            }
        }

        [Fact]
        public void SinTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(MathF.Sin).ToArray();
            var res = Float32Tensor.From(data).Sin();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void CosTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(MathF.Cos).ToArray();
            var res = Float32Tensor.From(data).Cos();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void TanTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(MathF.Tan).ToArray();
            var res = Float32Tensor.From(data).Tan();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void SinhTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(MathF.Sinh).ToArray();
            var res = Float32Tensor.From(data).Sinh();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void CoshTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(MathF.Cosh).ToArray();
            var res = Float32Tensor.From(data).Cosh();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void TanhTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(MathF.Tanh).ToArray();
            var res = Float32Tensor.From(data).Tanh();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void AsinTest()
        {
            var data = new float[] { 1.0f, 0.2f, -0.1f };
            var expected = data.Select(MathF.Asin).ToArray();
            var res = Float32Tensor.From(data).Asin();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void AcosTest()
        {
            var data = new float[] { 1.0f, 0.2f, -0.1f };
            var expected = data.Select(MathF.Acos).ToArray();
            var res = Float32Tensor.From(data).Acos();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void AtanTest()
        {
            var data = new float[] { 1.0f, 0.2f, -0.1f };
            var expected = data.Select(MathF.Atan).ToArray();
            var res = Float32Tensor.From(data).Atan();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void LogTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(x => MathF.Log(x)).ToArray();
            var res = Float32Tensor.From(data).Log();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void Log10Test()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };
            var expected = data.Select(MathF.Log10).ToArray();
            var res = Float32Tensor.From(data).Log10();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void FloorTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };
            var expected = data.Select(MathF.Floor).ToArray();
            var res = Float32Tensor.From(data).Floor();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void CeilTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };
            var expected = data.Select(MathF.Ceiling).ToArray();
            var res = Float32Tensor.From(data).Ceil();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void RoundTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };
            var expected = data.Select(x => MathF.Round(x)).ToArray();
            var res = Float32Tensor.From(data).Round();
            Assert.True(res.AllClose(Float32Tensor.From(expected)));
        }

        [Fact]
        public void ExpandTest()
        {
            TorchTensor ones = Float32Tensor.Ones(new long[] { 2 });
            TorchTensor onesExpanded = ones.Expand(new long[] { 3, 2 });

            Assert.Equal(onesExpanded.Shape, new long[] { 3, 2 });
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Assert.Equal(1.0, onesExpanded[i, j].ToSingle());
                }
            }
        }

        [Fact]
        public void TopKTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };

            var res1 = Float32Tensor.From(data).TopK(1);
            var res1_0 = res1.values[0].ToSingle();
            var index1_0 = res1.indexes[0].ToInt64();
            Assert.Equal(3.1f, res1_0);
            Assert.Equal(2L, index1_0);

            var res2 = Float32Tensor.From(data).TopK(2, sorted: true);
            var res2_0 = res2.values[0].ToSingle();
            var index2_0 = res2.indexes[0].ToInt64();
            var res2_1 = res2.values[1].ToSingle();
            var index2_1 = res2.indexes[1].ToInt64();
            Assert.Equal(3.1f, res2_0);
            Assert.Equal(2L, index2_0);
            Assert.Equal(2.0f, res2_1);
            Assert.Equal(1L, index2_1);
        }

        [Fact]
        public void SumTest()
        {
            var data = new float[] { 1.0f, 2.0f, 3.0f };

            var res1 = Float32Tensor.From(data).Sum();
            var res1_0 = res1.ToSingle();
            Assert.Equal(6.0f, res1_0);

            var res2 = Float32Tensor.From(data).Sum(type: ScalarType.Float64);
            var res2_0 = res2.ToDouble();
            Assert.Equal(6.0, res2_0);

            // summing integers gives long unless type is explicitly specified
            var dataInt32 = new int[] { 1, 2, 3 };
            var res3 = Int32Tensor.From(dataInt32).Sum();
            Assert.Equal(ScalarType.Int64, res3.Type);
            var res3_0 = res3.ToInt64();
            Assert.Equal(6L, res3_0);

            // summing integers gives long unless type is explicitly specified
            var res4 = Int32Tensor.From(dataInt32).Sum(type: ScalarType.Int32);
            Assert.Equal(ScalarType.Int32, res4.Type);
            var res4_0 = res4.ToInt32();
            Assert.Equal(6L, res4_0);

        }

        [Fact]
        public void UnbindTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };

            var res = Float32Tensor.From(data).Unbind();
            Assert.Equal(3, res.Length);
            Assert.Equal(new long[] { }, res[0].Shape);
            Assert.Equal(new long[] { }, res[1].Shape);
            Assert.Equal(new long[] { }, res[2].Shape);
            Assert.Equal(1.1f, res[0].ToSingle());
            Assert.Equal(2.0f, res[1].ToSingle());
            Assert.Equal(3.1f, res[2].ToSingle());
        }

        [Fact]
        public void SplitWithSizesTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };

            var res = Float32Tensor.From(data).SplitWithSizes(new long[] { 2, 1 });
            Assert.Equal(2, res.Length);
            Assert.Equal(new long[] { 2 }, res[0].Shape);
            Assert.Equal(new long[] { 1 }, res[1].Shape);
            Assert.Equal(1.1f, res[0][0].ToSingle());
            Assert.Equal(2.0f, res[0][1].ToSingle());
            Assert.Equal(3.1f, res[1][0].ToSingle());
        }

        [Fact]
        public void RandomTest()
        {
            var res = Float32Tensor.Random(new long[] { 2 });
            Assert.Equal(new long[] { 2 }, res.Shape);

            var res1 = Int16Tensor.RandomIntegers(10, new long[] { 200 });
            Assert.Equal(new long[] { 200 }, res1.Shape);

            var res2 = Int32Tensor.RandomIntegers(10, new long[] { 200 });
            Assert.Equal(new long[] { 200 }, res2.Shape);

            var res3 = Int64Tensor.RandomIntegers(10, new long[] { 200 });
            Assert.Equal(new long[] { 200 }, res3.Shape);

            var res4 = ByteTensor.RandomIntegers(10, new long[] { 200 });
            Assert.Equal(new long[] { 200 }, res4.Shape);

            var res5 = Int8Tensor.RandomIntegers(10, new long[] { 200 });
            Assert.Equal(new long[] { 200 }, res5.Shape);

            var res6 = Float16Tensor.RandomIntegers(10, new long[] { 200 });
            Assert.Equal(new long[] { 200 }, res6.Shape);

            var res7 = BFloat16Tensor.RandomIntegers(10, new long[] { 200 });
            Assert.Equal(new long[] { 200 }, res7.Shape);

            //var res7 = ComplexFloat16Tensor.RandomIntegers(10, new long[] { 200 });
            //Assert.Equal(new long[] { 200 }, res7.Shape);

            //var res8 = ComplexFloat32Tensor.RandomIntegers(10, new long[] { 200 });
            //Assert.Equal(new long[] { 200 }, res8.Shape);

            //var res9 = ComplexFloat64Tensor.RandomIntegers(10, new long[] { 200 });
            //Assert.Equal(new long[] { 200 }, res9.Shape);
        }

        [Fact]
        public void SqueezeTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };

            var res = Float32Tensor.From(data).Expand(new long[] { 1, 1, 3 }).Squeeze(0).Squeeze(0);
            Assert.Equal(new long[] { 3 }, res.Shape);
            Assert.Equal(1.1f, res[0].ToSingle());
            Assert.Equal(2.0f, res[1].ToSingle());
            Assert.Equal(3.1f, res[2].ToSingle());
        }

        [Fact]
        public void NarrowTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f };

            var res = Float32Tensor.From(data).Narrow(0, 1, 2);
            Assert.Equal(new long[] { 2 }, res.Shape);
            Assert.Equal(2.0f, res[0].ToSingle());
            Assert.Equal(3.1f, res[1].ToSingle());
        }

        [Fact]
        public void SliceTest()
        {
            var data = new float[] { 1.1f, 2.0f, 3.1f, 4.0f };

            var res = Float32Tensor.From(data).Slice(0, 1, 1, 1);
            Assert.Equal(new long[] { 0 }, res.Shape);

            var res2 = Float32Tensor.From(data).Slice(0, 1, 2, 1);
            Assert.Equal(new long[] { 1 }, res2.Shape);
            Assert.Equal(2.0f, res2[0].ToSingle());

            var res3 = Float32Tensor.From(data).Slice(0, 1, 4, 2);
            Assert.Equal(new long[] { 2 }, res3.Shape);
            Assert.Equal(2.0f, res3[0].ToSingle());
            Assert.Equal(4.0f, res3[1].ToSingle());
        }
        [Fact]
        public void Conv1DTest()
        {
            var t1 =
                new float[3, 4, 5]
                   {{{0.3460f, 0.4414f, 0.2384f, 0.7905f, 0.2267f},
                                     {0.5161f, 0.9032f, 0.6741f, 0.6492f, 0.8576f},
                                     {0.3373f, 0.0863f, 0.8137f, 0.2649f, 0.7125f},
                                     {0.7144f, 0.1020f, 0.0437f, 0.5316f, 0.7366f}},

                                    {{0.9871f, 0.7569f, 0.4329f, 0.1443f, 0.1515f},
                                     {0.5950f, 0.7549f, 0.8619f, 0.0196f, 0.8741f},
                                     {0.4595f, 0.7844f, 0.3580f, 0.6469f, 0.7782f},
                                     {0.0130f, 0.8869f, 0.8532f, 0.2119f, 0.8120f}},

                                    {{0.5163f, 0.5590f, 0.5155f, 0.1905f, 0.4255f},
                                     {0.0823f, 0.7887f, 0.8918f, 0.9243f, 0.1068f},
                                     {0.0337f, 0.2771f, 0.9744f, 0.0459f, 0.4082f},
                                     {0.9154f, 0.2569f, 0.9235f, 0.9234f, 0.3148f}}};
            var t2 =
                new float[2, 4, 3]
                   {{{0.4941f, 0.8710f, 0.0606f},
                     {0.2831f, 0.7930f, 0.5602f},
                     {0.0024f, 0.1236f, 0.4394f},
                     {0.9086f, 0.1277f, 0.2450f}},

                    {{0.5196f, 0.1349f, 0.0282f},
                     {0.1749f, 0.6234f, 0.5502f},
                     {0.7678f, 0.0733f, 0.3396f},
                     {0.6023f, 0.6546f, 0.3439f}}};

            var t1raw = new float[3 * 4 * 5];
            var t2raw = new float[2 * 4 * 3];
            { for (int i = 0; i < 3; i++) for (int j = 0; j < 4; j++) for (int k = 0; k < 5; k++) { t1raw[i * 4 * 5 + j * 5 + k] = t1[i, j, k]; } }
            { for (int i = 0; i < 2; i++) for (int j = 0; j < 4; j++) for (int k = 0; k < 3; k++) { t2raw[i * 4 * 3 + j * 3 + k] = t2[i, j, k]; } }
            var t1t = Float32Tensor.From(t1raw, new long[] { 3, 4, 5 });
            var t2t = Float32Tensor.From(t2raw, new long[] { 2, 4, 3 });
            var t3t = t1t.Conv1D(t2t);

            // Check the answer
            var t3Correct =
                new float[3, 2, 3]
                    {{{2.8516f, 2.0732f, 2.6420f},
                      {2.3239f, 1.7078f, 2.7450f}},

                    {{3.0127f, 2.9651f, 2.5219f},
                     {3.0899f, 3.1496f, 2.4110f}},

                    {{3.4749f, 2.9038f, 2.7131f},
                     {2.7692f, 2.9444f, 3.2554f}}};
            {
                var data = t3t.Data<float>();
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                        for (int k = 0; k < 3; k++)
                        {
                            var itemCorrect = t3Correct[i, j, k];
                            var item = data[i * 2 * 3 + j * 3 + k];
                            Assert.True(Math.Abs(itemCorrect - item) < 0.01f);
                        }
            }
            
            var t3p2d3 = t1t.Conv1D(t2t, padding: 2, dilation: 3);

            // Check the answer
            var t3p2d3Correct =
                new float[3, 2, 3]
                    {{{ 2.1121f, 0.8484f, 2.2709f},
                      {1.6692f, 0.5406f, 1.8381f}},

                     {{2.5078f, 1.2137f, 0.9173f},
                      {2.2395f, 1.1805f, 1.1954f}},

                     {{1.5215f, 1.3946f, 2.1327f},
                      {1.0732f, 1.3014f, 2.0696f}}};
            {
                var data = t3p2d3.Data<float>();
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 2; j++)
                        for (int k = 0; k < 3; k++)
                        {
                            var itemCorrect = t3p2d3Correct[i, j, k];
                            var item = data[i * 2 * 3 + j * 3 + k];
                            Assert.True(Math.Abs(itemCorrect - item) < 0.01f);
                        }
            }
        }
    }
}
