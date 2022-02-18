".NET image rule definitions"

load("@io_bazel_rules_pkg//pkg:pkg.bzl", "pkg_tar")
load("@io_bazel_rules_docker//container:container.bzl", "container_image")

def dotnet_image(name, base, binary, entrypoint = None, **kwargs):
    """Constructs a container image using a .NET binary target.

    Args:
        name: The name of the target.
        base: The base layers on top of which to overlay this layer,
            equivalent to FROM.
        binary: The csharp_binary or fsharp_binary target to use.
        entrypoint: An entrypoint for the image, equivalent to ENTRYPOINT.

            Set to None, [] or "" will set the entrypoint of the image to
            be null.
        **kwargs: See [container_image](https://github.com/bazelbuild/rules_docker/blob/master/docs/container.md#container_image).
    """
    pkg = name + ".pkg"
    container_image(
        name = name,
        base = base,
        entrypoint = entrypoint,
        tars = [":" + pkg],
        workdir = "/app",
        **kwargs
    )
    pkg_tar(
        name = pkg,
        srcs = [binary],
        include_runfiles = True,
        mode = "0755",
        package_dir = "/app",
        visibility = kwargs.get("visibility", None),
        restricted_to = kwargs.get("restricted_to", None),
        compatible_with = kwargs.get("compatible_with", None),
    )
