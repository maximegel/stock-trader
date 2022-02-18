PACKAGE_NAME=$1
VERSION=$2

bazel run @io_bazel_rules_dotnet//tools/nuget2bazel:nuget2bazel.exe -- \
  add \
  -p ${PWD} \
  -b deps.bzl \
  -c deps.json \
  -i -l \
  ${PACKAGE_NAME} ${VERSION}
