# Run each time the container is successfully created.
# see https://code.visualstudio.com/docs/remote/devcontainerjson-reference#_lifecycle-scripts

export WORKSPACE_FOLDER=${PWD}

# Export WORKSPACE_FOLDER variable in shell profile.
echo "export WORKSPACE_FOLDER=${WORKSPACE_FOLDER}" | sudo tee -a /etc/profile
# Include workspace's shell profile.
echo "source ${WORKSPACE_FOLDER}/.envrc" | sudo tee -a /etc/profile
# Reload bash profile.
source /etc/profile

# Set zsh as the default shell.
sudo chsh -s $(which zsh) $USER
# Clear user's zsh profile.
echo "" > ~/.zshrc
# Export WORKSPACE_FOLDER variable in zsh profile.
echo "export WORKSPACE_FOLDER=${WORKSPACE_FOLDER}" >> ~/.zshrc
# Include workspace's zsh profile.
echo "source ${WORKSPACE_FOLDER}/tools/zsh/.zshrc" >> ~/.zshrc
# Reload zsh profile.
source ~/.zshrc
# Install zsh plugins dependencies here:
sudo apt-get -y install direnv
# Install zsh powerlevel10k theme.
# See https://github.com/romkatv/powerlevel10k
git clone --depth=1 https://github.com/romkatv/powerlevel10k.git \
  ${ZSH_CUSTOM:-$HOME/.oh-my-zsh/custom}/themes/powerlevel10k
# Restart zsh.
exec zsh
