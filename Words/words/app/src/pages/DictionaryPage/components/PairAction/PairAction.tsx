import { useState } from "react";
import { useNavigate } from "react-router";
import { useStore } from "../../../../store/useStore";
import type { WordsPair } from "../../../../types/Pair";
import {
  IconButton,
  Menu,
  MenuItem,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import { MenuIcon, EditIcon, DeleteIcon } from "./Icons";


interface DictionaryActionProps {
  pair: WordsPair;
}
export const PairAction = ({ pair }: DictionaryActionProps) => {
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const navigate = useNavigate();
  const removePair = useStore((state) => state.remove);

  const handleMenuOpen = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleEdit = () => {
    handleMenuClose();
    navigate("/editpair", { state: { pair } });
  };

  const handleDelete = () => {
    handleMenuClose();
    removePair(pair);
  };

  return (
    <div className="actionContainer">
      <IconButton
        aria-label="actions"
        onClick={handleMenuOpen}
        className="menuButton"
      >
        <MenuIcon />
      </IconButton>

      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
        anchorOrigin={{
          vertical: "bottom",
          horizontal: "right",
        }}
        transformOrigin={{
          vertical: "top",
          horizontal: "right",
        }}
      >
        <MenuItem onClick={handleEdit} className="menuItem">
          <ListItemIcon className="menuIcon">
            <EditIcon fontSize="small" />
          </ListItemIcon>
          <ListItemText>Редактировать</ListItemText>
        </MenuItem>
        <MenuItem onClick={handleDelete} className="menuItem">
          <ListItemIcon className="menuIcon">
            <DeleteIcon fontSize="small" />
          </ListItemIcon>
          <ListItemText>Удалить</ListItemText>
        </MenuItem>
      </Menu>
    </div>
  );
};
