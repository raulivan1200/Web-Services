import Group from "@mui/icons-material/Group";
import { Box, AppBar, Toolbar, Typography, Button, Container, MenuItem, MenuList } from "@mui/material";

type Props = {
  openForm: () => void;
  disabled?: boolean;
}

export default function NavBar({ openForm, disabled = false }: Props) {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar position="static"
        sx={{ backgroundImage: "linear-gradient(135deg, red 0%, blue 70%, green 90%)" }}>
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <MenuList>
              <MenuItem sx={{ display: "flex", gap: 2 }}>
                <Group fontSize="large" />
                <Typography variant="h4" sx={{ fontWeight: "bold" }}>Activities</Typography>
              </MenuItem>
            </MenuList>
            <MenuList disablePadding sx={{ display: "flex" }}>
              <MenuItem sx={{ fontSize: "1.2rem", textTransform: "uppercase" }}>Activities</MenuItem>
              <MenuItem sx={{ fontSize: "1.2rem", textTransform: "uppercase" }}>About</MenuItem>
              <MenuItem sx={{ fontSize: "1.2rem", textTransform: "uppercase" }}>Contact</MenuItem>
            </MenuList>
            <Button onClick={openForm} disabled={disabled} size="large" variant="contained" color="warning">Create activity</Button>
          </Toolbar>
        </Container>
      </AppBar>
    </Box>
  )
}
