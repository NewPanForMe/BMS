import { NIcon } from "naive-ui";
import {    PersonCircleOutline as UserIcon,    Pencil as EditIcon,    LogOutOutline as LogoutIcon} from "@vicons/ionicons5";
import { h } from "vue";



function renderIcon(icon) {
    return () => h(NIcon, null, { default: () => h(icon) });
}


const options = [
    {
        label: "用户资料",
        key: "profile",
        icon: renderIcon(UserIcon)
    },
    {
        label: "编辑用户资料",
        key: "editProfile",
        icon: renderIcon(EditIcon)
    },
    {
        label: "退出登录",
        key: "logout",
        icon: renderIcon(LogoutIcon)
    }
];


export default options