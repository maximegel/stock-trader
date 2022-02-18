".NET rule definitions"

load(
    "//tools/bazel/docker/dotnet/private:image.bzl",
    _dotnet_image = "dotnet_image",
)

dotnet_image = _dotnet_image
