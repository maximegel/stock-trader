# Path to your oh-my-zsh installation.
export ZSH=$HOME/.oh-my-zsh

# Set name of the theme to load.
# See https://github.com/ohmyzsh/ohmyzsh/wiki/Themes
ZSH_THEME="powerlevel10k/powerlevel10k"

# Set plugins to load.
# See https://github.com/ohmyzsh/ohmyzsh/wiki/Plugins
plugins=(
  aliases
  bazel
  docker
  git
  kubectl
  minikube
  vscode
)

source $ZSH/oh-my-zsh.sh

DISABLE_AUTO_UPDATE=true
DISABLE_UPDATE_PROMPT=true

# Include powerlevel10k theme configuration.
# See https://github.com/romkatv/powerlevel10k
source $WORKSPACE_FOLDER/tools/zsh/.p10k.zsh

# Include workspace's shell profile.
source ${WORKSPACE_FOLDER}/.envrc
